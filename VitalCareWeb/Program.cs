using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VitalCareWeb;
using VitalCareWeb.Data;
using VitalCareWeb.Extensions;
using VitalCareWeb.Services.Appointment;
using VitalCareWeb.Services.AppointmentReason;
using VitalCareWeb.Services.Article;
using VitalCareWeb.Services.ArticleCategory;
using VitalCareWeb.Services.Brand;
using VitalCareWeb.Services.CounterRecord;
using VitalCareWeb.Services.Doctor;
using VitalCareWeb.Services.EmailService;
using VitalCareWeb.Services.Inquiry;
using VitalCareWeb.Services.Location;
using VitalCareWeb.Services.Product;
using VitalCareWeb.Services.Serivice;
using VitalCareWeb.Services.Speciality;
using VitalCareWeb.Services.Tag;
using VitalCareWeb.Services.WhyChooseUsRecord;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureEmailService(builder.Configuration);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddRazorRuntimeCompilation();

//----- Services Manage
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IAppointmentReasonService, AppointmentReasonService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IInquiryService, InquiryService>();
builder.Services.AddScoped<IWhyChooseUsRecordService, WhyChooseUsRecordService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICounterRecordService, CounterRecordService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//---- Mapper Configuration
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
