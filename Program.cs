using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.OData.ModelBuilder;
using ODataGroupByDerivedTypeIssue.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(o => o.Filter());

WebApplication app = builder.Build();

app.MapControllers();

app.Use((context, next) =>
{
    ODataModelBuilder oDataModelBuilder = new();

    EntityTypeConfiguration<Product> productType = oDataModelBuilder.EntityType<Product>();
    productType.HasKey(p => new { p.ProductName });
    productType.Property(p => p.ProductName);
    productType.ComplexProperty(p => p.Category);

    ComplexTypeConfiguration<ICategory> iCategoryType = oDataModelBuilder.ComplexType<ICategory>();

    ComplexTypeConfiguration<Category> categoryType = oDataModelBuilder.ComplexType<Category>();
    categoryType.DerivesFrom<ICategory>();
    categoryType.Property(c => c.CategoryName);

    oDataModelBuilder.EntitySet<Product>("Product");

    context.ODataFeature().Model = oDataModelBuilder.GetEdmModel();

    return next();
});


app.Run();



