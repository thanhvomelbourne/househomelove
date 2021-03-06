USE [master]
GO
/****** Object:  Database [househomelove]    Script Date: 23/01/2019 12:54:35 ******/
CREATE DATABASE [househomelove]
 WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS
GO
ALTER DATABASE [househomelove] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [househomelove].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [househomelove] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [househomelove] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [househomelove] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [househomelove] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [househomelove] SET ARITHABORT OFF 
GO
ALTER DATABASE [househomelove] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [househomelove] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [househomelove] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [househomelove] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [househomelove] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [househomelove] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [househomelove] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [househomelove] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [househomelove] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [househomelove] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [househomelove] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [househomelove] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [househomelove] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [househomelove] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [househomelove] SET  MULTI_USER 
GO
ALTER DATABASE [househomelove] SET DB_CHAINING OFF 
GO
ALTER DATABASE [househomelove] SET ENCRYPTION ON
GO
ALTER DATABASE [househomelove] SET QUERY_STORE = ON
GO
ALTER DATABASE [househomelove] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [househomelove]
GO
/****** Object:  Table [dbo].[ApplicationSettings]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[NewslettersDescription] [nvarchar](max) NULL,
	[BannerQuote1] [nvarchar](50) NULL,
	[BannerQuote2] [nvarchar](50) NULL,
	[BannerQuote3] [nvarchar](max) NULL,
	[BannerImage1] [nvarchar](max) NULL,
	[BannerImage2] [nvarchar](max) NULL,
	[BannerImage3] [nvarchar](max) NULL,
	[PromotionImage1] [nvarchar](max) NULL,
	[PromotionImage2] [nvarchar](max) NULL,
	[PromotionImage3] [nvarchar](max) NULL,
	[AdsIcon1] [nvarchar](70) NULL,
	[AdsTitle1] [nvarchar](70) NULL,
	[AdsIcon2] [nvarchar](70) NULL,
	[AdsTitle2] [nvarchar](70) NULL,
	[AdsIcon3] [nvarchar](70) NULL,
	[AdsTitle3] [nvarchar](70) NULL,
	[AdsIcon4] [nvarchar](70) NULL,
	[AdsTitle4] [nvarchar](70) NULL,
	[AdsIcon5] [nvarchar](70) NULL,
	[AdsTitle5] [nvarchar](70) NULL,
	[AdsIcon6] [nvarchar](70) NULL,
	[AdsTitle6] [nvarchar](70) NULL,
	[ServiceIcon1] [nvarchar](20) NULL,
	[ServiceTitle1] [nvarchar](50) NULL,
	[ServiceDescription1] [nvarchar](150) NULL,
	[ServiceIcon2] [nvarchar](20) NULL,
	[ServiceTitle2] [nvarchar](50) NULL,
	[ServiceDescription2] [nvarchar](150) NULL,
	[ServiceIcon3] [nvarchar](20) NULL,
	[ServiceTitle3] [nvarchar](50) NULL,
	[ServiceDescription3] [nvarchar](150) NULL,
	[ServiceIcon4] [nvarchar](20) NULL,
	[ServiceTitle4] [nvarchar](50) NULL,
	[ServiceDescription4] [nvarchar](150) NULL,
	[PartnerLogo1] [nvarchar](max) NULL,
	[PartnerLogo2] [nvarchar](max) NULL,
	[PartnerLogo3] [nvarchar](max) NULL,
	[PartnerLogo4] [nvarchar](max) NULL,
	[TermsAndConditions] [nvarchar](max) NULL,
	[FAQ] [nvarchar](max) NULL,
	[ReturnPolicy] [nvarchar](max) NULL,
	[AdminEmail] [nvarchar](max) NULL,
	[CustomerServiceEmail] [nvarchar](max) NULL,
	[ECommerceEmail] [nvarchar](max) NULL,
	[PaymentReceiptEmail] [nvarchar](max) NULL,
	[GSTPercent] [float] NOT NULL,
	[CreditCardSurcharge] [float] NOT NULL,
	[PaypalSurcharge] [float] NOT NULL,
	[FreeShippingAusPostFrom] [float] NULL,
	[FreeShippingHomeMailFrom] [float] NULL,
	[AusPostDescription] [nvarchar](500) NULL,
	[HomeMailDescription] [nvarchar](500) NULL,
	[HomeMailDefaultPrice1] [float] NOT NULL,
	[HomeMailDefaultPrice2] [float] NOT NULL,
	[HomeMailDefaultPrice3] [float] NOT NULL,
	[HomeMailAvailableFrom] [float] NOT NULL,
	[WarehousePostcode] [int] NOT NULL,
	[PopularSearchTag] [nvarchar](150) NULL,
	[CurrencyRateUSDToAUD] [float] NULL,
	[RateCalculateOriginalPrice] [float] NULL,
	[RateCalculateDiscountPrice] [float] NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[ClickAndCollectDescription] [nvarchar](500) NULL,
	[MetaDescription] [nvarchar](500) NULL,
 CONSTRAINT [PK_ApplicationSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[UnitOfPrice] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[OriginalPrice] [float] NOT NULL,
	[DiscountPrice] [float] NOT NULL,
	[FinalPrice] [float] NOT NULL,
	[PrimaryImage] [nvarchar](250) NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](256) NULL,
	[PrefixForProductCode] [nvarchar](50) NULL,
	[IsLive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactName] [nvarchar](100) NOT NULL,
	[ContactEmail] [nvarchar](100) NULL,
	[ContactPhone] [nvarchar](15) NULL,
	[Subject] [nvarchar](300) NULL,
	[Message] [nvarchar](max) NULL,
	[IsRead] [bit] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EVoucher]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EVoucher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromFirstName] [nvarchar](50) NOT NULL,
	[FromLastName] [nvarchar](50) NOT NULL,
	[FromEmail] [nvarchar](50) NOT NULL,
	[FromPhone] [nvarchar](50) NULL,
	[Message] [nvarchar](500) NULL,
	[Amount] [float] NOT NULL,
	[Balance] [float] NOT NULL,
	[IsGift] [bit] NOT NULL,
	[ToFirstName] [nvarchar](50) NULL,
	[ToLastName] [nvarchar](50) NULL,
	[ToEmail] [nvarchar](50) NULL,
	[ToPhone] [nvarchar](50) NULL,
	[EVoucherSerialNo] [nvarchar](50) NOT NULL,
	[ActivationCode] [nvarchar](50) NOT NULL,
	[IsLive] [bit] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_EVoucher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LikedProduct]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LikedProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_LikedProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [int] NOT NULL,
	[UserId] [int] NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Suburb] [nvarchar](20) NOT NULL,
	[Postcode] [nvarchar](10) NOT NULL,
	[State] [nvarchar](20) NOT NULL,
	[Country] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[DeliveryFirstName] [nvarchar](50) NULL,
	[DeliveryLastName] [nvarchar](50) NULL,
	[DeliveryCompanyName] [nvarchar](50) NULL,
	[DeliveryAddress] [nvarchar](100) NULL,
	[DeliverySuburb] [nvarchar](20) NULL,
	[DeliveryPostcode] [nvarchar](10) NULL,
	[DeliveryState] [nvarchar](20) NULL,
	[DeliveryCountry] [nvarchar](20) NULL,
	[Note] [nvarchar](1000) NULL,
	[ConfirmedStatus] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CompletedAt] [datetime] NULL,
	[TrackingNumber] [nvarchar](50) NULL,
	[ShippingMethod] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[Method] [nvarchar](20) NOT NULL,
	[Amount] [float] NOT NULL,
	[Success] [bit] NOT NULL,
	[IsFull] [bit] NOT NULL,
	[Reason] [nvarchar](250) NULL,
	[Detail] [nvarchar](250) NULL,
	[IPAddress] [nchar](20) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreOrder]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ProductId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[CustomerEscalate] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_PreOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[OriginalProductCode] [nvarchar](50) NULL,
	[Namekey] [nvarchar](20) NULL,
	[ColorCode] [nvarchar](20) NULL,
	[ProductCode] [nvarchar](40) NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Weight] [float] NOT NULL,
	[Height] [float] NOT NULL,
	[Width] [float] NOT NULL,
	[Length] [float] NOT NULL,
	[MoreInfo] [nvarchar](max) NULL,
	[InStock] [nvarchar](15) NULL,
	[StockLevel] [int] NOT NULL,
	[IsLive] [bit] NOT NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[UnitOfPrice] [nvarchar](50) NOT NULL,
	[ImportPriceUSD] [float] NULL,
	[ImportPrice] [float] NOT NULL,
	[OriginalPrice] [float] NOT NULL,
	[DiscountPrice] [float] NOT NULL,
	[NumberOfRating] [int] NOT NULL,
	[NumberOfLiked] [int] NOT NULL,
	[NumberOfReviewed] [int] NOT NULL,
	[AverageRating] [float] NOT NULL,
	[PrimaryImage] [nvarchar](250) NULL,
	[SubImage1] [nvarchar](250) NULL,
	[SubImage2] [nvarchar](250) NULL,
	[SubImage3] [nvarchar](250) NULL,
	[SubImage4] [nvarchar](250) NULL,
	[SubImage5] [nvarchar](250) NULL,
	[ExchangeRate] [float] NULL,
	[OriginalRate] [float] NULL,
	[DiscountRate] [float] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[SubCategoryId1] [int] NULL,
	[SubCategoryId2] [int] NULL,
	[Materials] [nvarchar](100) NULL,
	[Colors] [nvarchar](100) NULL,
	[ShareLink] [nvarchar](250) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotions]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PromotionType] [int] NOT NULL,
	[NameOfPromotion] [nvarchar](100) NOT NULL,
	[PromotionCode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Percentage] [float] NULL,
	[Dollars] [float] NULL,
	[AutoApply] [bit] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[LimitTime] [int] NULL,
	[IsLive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Promotions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewedProduct]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewedProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](500) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_ReviewedProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCart]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalAmountExGst] [float] NOT NULL,
	[TotalAmountIncGst] [float] NOT NULL,
	[Gst] [float] NOT NULL,
	[ShippingFee] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[TotalFinalPrice] [float] NOT NULL,
	[EstimateDelivery] [nvarchar](500) NULL,
	[PromotionId] [int] NULL,
	[ShippingPromotionId] [int] NULL,
	[GSTPercent] [float] NOT NULL,
	[CreditCardSurcharge] [float] NOT NULL,
	[PayPalSurcharge] [float] NOT NULL,
	[FreeShippingAusPostFrom] [float] NULL,
	[FreeShippingHomeMailFrom] [float] NULL,
	[HomeMailDefaultPrice1] [float] NULL,
	[HomeMailDefaultPrice2] [float] NULL,
	[HomeMailDefaultPrice3] [float] NULL,
	[HomeMailAvailableFrom] [float] NULL,
	[WarehousePostcode] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subcriptions]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subcriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[IsCustomer] [bit] NULL,
	[EmailSubcribed] [nvarchar](150) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Subcription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Gender] [bit] NOT NULL,
	[CompanyName] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[Suburb] [nvarchar](20) NULL,
	[Postcode] [nvarchar](10) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](20) NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Avatar] [nvarchar](max) NULL,
	[IsLive] [bit] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
 CONSTRAINT [PK_webpages_Membership] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_webpages_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_webpages_UsersInRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlist]    Script Date: 23/01/2019 12:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlist](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Wishlist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ApplicationSettings] ON 

INSERT [dbo].[ApplicationSettings] ([Id], [StoreName], [Address], [Phone], [Email], [NewslettersDescription], [BannerQuote1], [BannerQuote2], [BannerQuote3], [BannerImage1], [BannerImage2], [BannerImage3], [PromotionImage1], [PromotionImage2], [PromotionImage3], [AdsIcon1], [AdsTitle1], [AdsIcon2], [AdsTitle2], [AdsIcon3], [AdsTitle3], [AdsIcon4], [AdsTitle4], [AdsIcon5], [AdsTitle5], [AdsIcon6], [AdsTitle6], [ServiceIcon1], [ServiceTitle1], [ServiceDescription1], [ServiceIcon2], [ServiceTitle2], [ServiceDescription2], [ServiceIcon3], [ServiceTitle3], [ServiceDescription3], [ServiceIcon4], [ServiceTitle4], [ServiceDescription4], [PartnerLogo1], [PartnerLogo2], [PartnerLogo3], [PartnerLogo4], [TermsAndConditions], [FAQ], [ReturnPolicy], [AdminEmail], [CustomerServiceEmail], [ECommerceEmail], [PaymentReceiptEmail], [GSTPercent], [CreditCardSurcharge], [PaypalSurcharge], [FreeShippingAusPostFrom], [FreeShippingHomeMailFrom], [AusPostDescription], [HomeMailDescription], [HomeMailDefaultPrice1], [HomeMailDefaultPrice2], [HomeMailDefaultPrice3], [HomeMailAvailableFrom], [WarehousePostcode], [PopularSearchTag], [CurrencyRateUSDToAUD], [RateCalculateOriginalPrice], [RateCalculateDiscountPrice], [UpdatedAt], [UpdatedBy], [ClickAndCollectDescription], [MetaDescription]) VALUES (2, N'House Home Love', N'Deer Park, Victoria 3023, Australia', N'(+61) 425.88.99.39', N'admin@househomelove.com.au', N'Discover the aspects of handmade furniture and home decor products.', N'WELCOME TO ', N'HOUSE HOME LOVE', N'House is not a Home until there is Love', N'/images/banner_icon_images/Banner18586577141291015955slider1.jpg', N'/images/banner_icon_images/Banner28586577141290856385slider2.jpg', N'', N'/images/banner_icon_images/Promotion18586577671769916922IMG_3135.jpg', N'/images/banner_icon_images/Promotion28586575506037507752discount1.png', N'/images/banner_icon_images/Promotion38586575506037467736IMG_3133.jpg', N'/images/banner_icon_images/AdsIcon18586534015262339489truck.svg', N'Free shipping in Australia over $299', N'/images/banner_icon_images/AdsIcon28586534015262183208voucher2.svg', N'E-VOUCHER IS READY FOR YOUR PRESENT', N'/images/banner_icon_images/AdsIcon38586534014261982963email2.svg', N'SUBSCRIPTION FOR THE LATEST PRODUCTS', N'/images/banner_icon_images/AdsIcon48586534014261826723bamboo.svg', N'HANDMADE FROM BAMBOO IN VIETNAM', N'/images/banner_icon_images/AdsIcon18586534014261670242house.svg', N'WE MAKE YOUR HOUSE BECOME MORE FRIENDLY', N'/images/banner_icon_images/AdsIcon18586534014261514169guarantee.svg', N'OUR PRICE BEAT GUARANTEE', N'pe-7s-world', N'FREE SHIPPING', N'Free shipping in Australia over $299', N'pe-7s-refresh', N'FREE EXCHANGE', N'10 days return on all items', N'pe-7s-headphones', N'PREMIUM SUPPORT', N'We support online 7 days a week', N'pe-7s-gift', N'E-VOUCHER', N'Meaningful gift for your family', N'/images/banner_icon_images/Partner18586534013799737463clickandcollect.svg', N'/images/banner_icon_images/Partner28586534013799602558homemail.svg', N'/images/banner_icon_images/Partner38586534013799602558paypal.png', N'/images/banner_icon_images/Partner48586534013088613285partner_auspost_logo.png', N'<h2>Welcome to House Home Love</h2>

<p>These terms and conditions outline the rules and regulations for the use of House Home Love&#39;s Website.<br />
House Home Love is located at:</p>

<address>Sunshine North, VIC&nbsp;3020, Australia</address>

<p>By accessing this website we assume you accept these terms and conditions in full. Do not continue to use House Home Love&#39;s website if you do not accept all of the terms and conditions stated on this page.</p>

<p>The following terminology applies to these Terms and Conditions, Privacy Statement and Disclaimer Notice and any or all Agreements: &ldquo;Client&rdquo;, &ldquo;You&rdquo; and &ldquo;Your&rdquo; refers to you, the person accessing this website and accepting the Company&rsquo;s terms and conditions. &ldquo;The Company&rdquo;, &ldquo;Ourselves&rdquo;, &ldquo;We&rdquo;, &ldquo;Our&rdquo; and &ldquo;Us&rdquo;, refers to our Company. &ldquo;Party&rdquo;, &ldquo;Parties&rdquo;, or &ldquo;Us&rdquo;, refers to both the Client and ourselves, or either the Client or ourselves. All terms refer to the offer, acceptance and consideration of payment necessary to undertake the process of our assistance to the Client in the most appropriate manner, whether by formal meetings of a fixed duration, or any other means, for the express purpose of meeting the Client&rsquo;s needs in respect of provision of the Company&rsquo;s stated services/products, in accordance with and subject to, prevailing law of Australia. Any use of the above terminology or other words in the singular, plural, capitalization and/or he/she or they, are taken as interchangeable and therefore as referring to same.</p>

<h2>Cookies</h2>

<p>We employ the use of cookies. By using House Home Love&#39;s website you consent to the use of cookies in accordance with House Home Love&rsquo;s privacy policy.</p>

<p>Most of the modern day interactive web sites use cookies to enable us to retrieve user details for each visit. Cookies are used in some areas of our site to enable the functionality of this area and ease of use for those people visiting. Some of our affiliate/advertising partners may also use cookies.</p>

<h2>License</h2>

<p>Unless otherwise stated, House Home Love and/or it&rsquo;s licensors own the intellectual property rights for all material on House Home Love. All intellectual property rights are reserved. You may view and/or print pages from http://househomelove.com.au for your own personal use subject to restrictions set in these terms and conditions.</p>

<p>You must not:</p>

<ol>
	<li>Republish material from http://househomelove.com.au</li>
	<li>Sell, rent or sub-license material from http://househomelove.com.au</li>
	<li>Reproduce, duplicate or copy material from http://househomelove.com.au</li>
</ol>

<p>Redistribute content from House Home Love (unless the content is specifically made for redistribution).</p>

<h2>Hyperlinking to our Content</h2>

<ol>
	<li>The following organizations may link to our Web site without prior written approval:
	<ol>
		<li>Government agencies;</li>
		<li>Search engines;</li>
		<li>News organizations;</li>
		<li>Online directory distributors when they list us in the directory may link to our Web site in the same manner as they hyperlink to the Web sites of other listed businesses; and</li>
		<li>Systemwide Accredited Businesses except soliciting non-profit organizations, charity shopping malls, and charity fundraising groups which may not hyperlink to our Web site.</li>
	</ol>
	</li>
</ol>

<ol start="2">
	<li>These organizations may link to our home page, to publications or to other Web site information so long as the link: (a) is not in any way misleading; (b) does not falsely imply sponsorship, endorsement or approval of the linking party and its products or services; and (c) fits within the context of the linking party&#39;s site.</li>
	<li>We may consider and approve in our sole discretion other link requests from the following types of organizations:
	<ol>
		<li>commonly-known consumer and/or business information sources such as Chambers of Commerce, American Automobile Association, AARP and Consumers Union;</li>
		<li>dot.com community sites;</li>
		<li>associations or other groups representing charities, including charity giving sites,</li>
		<li>online directory distributors;</li>
		<li>internet portals;</li>
		<li>accounting, law and consulting firms whose primary clients are businesses; and</li>
		<li>educational institutions and trade associations.</li>
	</ol>
	</li>
</ol>

<p>We will approve link requests from these organizations if we determine that: (a) the link would not reflect unfavorably on us or our accredited businesses (for example, trade associations or other organizations representing inherently suspect types of business, such as work-at-home opportunities, shall not be allowed to link); (b)the organization does not have an unsatisfactory record with us; (c) the benefit to us from the visibility associated with the hyperlink outweighs the absence of <!--?=$companyName?-->; and (d) where the link is in the context of general resource information or is otherwise consistent with editorial content in a newsletter or similar product furthering the mission of the organization.</p>

<p>These organizations may link to our home page, to publications or to other Web site information so long as the link: (a) is not in any way misleading; (b) does not falsely imply sponsorship, endorsement or approval of the linking party and it products or services; and (c) fits within the context of the linking party&#39;s site.</p>

<p>If you are among the organizations listed in paragraph 2 above and are interested in linking to our website, you must notify us by sending an e-mail to <a href="mailto:purpleskyvn@gmail.com" title="send an email to purpleskyvn@gmail.com">purpleskyvn@gmail.com</a>. Please include your name, your organization name, contact information (such as a phone number and/or e-mail address) as well as the URL of your site, a list of any URLs from which you intend to link to our Web site, and a list of the URL(s) on our site to which you would like to link. Allow 2-3 weeks for a response.</p>

<p>Approved organizations may hyperlink to our Web site as follows:</p>

<ol>
	<li>By use of our corporate name; or</li>
	<li>By use of the uniform resource locator (Web address) being linked to; or</li>
	<li>By use of any other description of our Web site or material being linked to that makes sense within the context and format of content on the linking party&#39;s site.</li>
</ol>

<p>No use of House Home Love&rsquo;s logo or other artwork will be allowed for linking absent a trademark license agreement.</p>

<h2>Iframes</h2>

<p>Without prior approval and express written permission, you may not create frames around our Web pages or use other techniques that alter in any way the visual presentation or appearance of our Web site.</p>

<h2>Reservation of Rights</h2>

<p>We reserve the right at any time and in its sole discretion to request that you remove all links or any particular link to our Web site. You agree to immediately remove all links to our Web site upon such request. We also reserve the right to amend these terms and conditions and its linking policy at any time. By continuing to link to our Web site, you agree to be bound to and abide by these linking terms and conditions.</p>

<h2>Removal of links from our website</h2>

<p>If you find any link on our Web site or any linked web site objectionable for any reason, you may contact us about this. We will consider requests to remove links but will have no obligation to do so or to respond directly to you.</p>

<p>Whilst we endeavor to ensure that the information on this website is correct, we do not warrant its completeness or accuracy; nor do we commit to ensuring that the website remains available or that the material on the website is kept up to date.</p>

<h2>Content Liability</h2>

<p>We shall have no responsibility or liability for any content appearing on your Web site. You agree to indemnify and defend us against all claims arising out of or based upon your Website. No link(s) may appear on any page on your Web site or within any context containing content or materials that may be interpreted as libelous, obscene or criminal, or which infringes, otherwise violates, or advocates the infringement or other violation of, any third party rights.</p>

<h2>Disclaimer</h2>

<p>To the maximum extent permitted by applicable law, we exclude all representations, warranties and conditions relating to our website and the use of this website (including, without limitation, any warranties implied by law in respect of satisfactory quality, fitness for purpose and/or the use of reasonable care and skill). Nothing in this disclaimer will:</p>

<ol>
	<li>limit or exclude our or your liability for death or personal injury resulting from negligence;</li>
	<li>limit or exclude our or your liability for fraud or fraudulent misrepresentation;</li>
	<li>limit any of our or your liabilities in any way that is not permitted under applicable law; or</li>
	<li>exclude any of our or your liabilities that may not be excluded under applicable law.</li>
</ol>

<p>The limitations and exclusions of liability set out in this Section and elsewhere in this disclaimer: (a) are subject to the preceding paragraph; and (b) govern all liabilities arising under the disclaimer or in relation to the subject matter of this disclaimer, including liabilities arising in contract, in tort (including negligence) and for breach of statutory duty.</p>

<p>To the extent that the website and the information and services on the website are provided free of charge, we will not be liable for any loss or damage of any nature.</p>

<h2>Credit &amp; Contact Information</h2>

<p>This Terms and conditions page was created at <a href="https://termsandconditionstemplate.com" style="color:inherit;text-decoration:none;cursor:text;">termsandconditionstemplate.com</a> generator. If you have any queries regarding any of our terms, please contact us.</p>
', NULL, N'<p style="text-align:justify"><span style="color:#000000">At Househomelove, we aim to deliver you the very best products at everyday low prices.</span></p>

<p style="text-align:justify"><span style="color:#000000">We understand that there may come a time where you need to return a purchase from us and we want to make the returns process as simple and easy as possible for you.</span></p>

<p style="text-align:justify"><span style="color:#000000">If you are not happy with your purchase from Househomelove, you can return or exchange it, or have it repaired, or a service cancelled, in accordance with the terms below.</span></p>

<h2><span style="font-size:18px"><span style="color:#ef546a">Change of Mind Returns &ndash; 30 Days</span></span></h2>

<p style="text-align:justify"><span style="color:#000000">If you have changed your mind about your purchase, Househomelove will be pleased to offer you a refund or exchange provided that:</span></p>

<ul>
	<li style="text-align:justify"><span style="color:#000000">You return the item within 30 days of purchase;</span></li>
	<li style="text-align:justify"><span style="color:#000000">You produce a satisfactory proof of purchase (being your original register receipt or online proof of purchase, such as a tax invoice);</span></li>
	<li style="text-align:justify"><span style="color:#000000">The item is in re-saleable condition, including its original packaging (if any), is unused and as sold; and</span></li>
	<li style="text-align:justify"><span style="color:#000000">A government issued form of identification is presented at the time of the return (for products with a value greater than $50).</span></li>
</ul>

<p style="text-align:justify"><span style="color:#000000">If you are unable to provide a satisfactory proof of purchase, Househomelovemay, at its absolute discretion, provide you with an exchange or Househomelove gift card to the current value of the item.</span></p>

<h2><span style="font-size:18px"><span style="color:#ef546a">Other Returns &ndash; Consumer Guarantees</span></span></h2>

<p style="text-align:justify"><span style="color:#000000">The Australian Consumer Law protects consumers by giving them certain guarantees when they buy goods and services. These are known as &ldquo;Consumer Guarantees&rdquo;. For further information, please refer to the&nbsp;</span><u><em><strong><a href="https://www.accc.gov.au/consumers/consumer-rights-guarantees"><span style="color:#000000">Australian Competition and Consumer Commission website</span></a></strong></em></u><span style="color:#000000">.</span></p>

<p style="text-align:justify"><span style="color:#000000">You must provide proof of purchase to make a claim and Househomelove reserves the right to decline an exchange, refund or repair where a fault is caused by misuse or neglect.</span></p>

<p style="text-align:justify"><span style="color:#000000">For Prepaid Business Services, you may cancel a service which you have activated and claim a refund for unused services if there is a problem with the service. Requests for the cancellation of a service may need to be submitted to the service provider.</span></p>

<p style="text-align:justify"><span style="color:#000000">If you experience activation problems with Prepaid Cards or Special Activation Codes provided by third parties, we recommend that you contact the third-party service provider for assistance in activating any credit entitlement, as Househomelove&nbsp;is unable to independently determine the validity of these codes.</span></p>

<h2><span style="font-size:18px"><span style="color:#ef546a">Manufacturer Assistance</span></span></h2>

<p style="text-align:justify"><span style="color:#000000">Many manufacturers have dedicated support centres designed specifically to deal with issues in relation to their products and may be able to provide a quicker assessment of, and remedy for, any issue with your product. You may, therefore, prefer to contact the manufacturer directly, although you are not obliged to do this. If at any time you are not satisfied with the manufacturers remedy in relation to your legal rights, you can contact your nearest Househomelove store. Househomelove may also return the product to the manufacturer to determine the nature of the problem.</span></p>

<h2><span style="font-size:18px"><span style="color:#ef546a">How to return your Househomelove purchase</span></span></h2>

<ul>
	<li style="text-align:justify"><span style="color:#000000">All Househomelove purchases can be returned at any Househomelove store Australia-wide.</span></li>
	<li style="text-align:justify"><span style="color:#000000">Refunds can only be paid in the same tender as the original purchase or refunded to the account used to pay for the item in the case of a 30-day business account holder.</span></li>
	<li style="text-align:justify"><span style="color:#000000">Where a credit card attached to an original purchase cannot be produced, the refund will be processed to an Househomelove Gift Card.</span></li>
</ul>
', N'thanhvo.melbourne@gmail.com', N'hannahvu11@gmail.com', N'thanhvo.melbourne@gmail.com', N'admin@househomelove.com.au', 0, 1.5, 1.5, 99.99, 79.99, N'We are shipping in Australia via Australia Post. The delivery price will be calculated based on every single item.', N'If you are in 20km radius from Melbourne CBD and your order over $99.95, you can choose our HomeMail service only from $7.55 - $11.55 for faster delivery. We commit delivery to you in 2 days includes weekend.', 7.55, 9.55, 11.55, 99.95, 3023, N'Basket, Table, Vase, Storage, Tray, Bowl', 1.5, 6, 5, CAST(N'2019-01-23T01:06:16.633' AS DateTime), N'admin', N'Order online and pick up in store in as little as 24 hours.', N'
Handmade Furniture From Bamboo | House Home Love | Melbourne, Australia | Discover the aspects of handmade bamboo, seagrass furniture and home decor products |')
SET IDENTITY_INSERT [dbo].[ApplicationSettings] OFF
SET IDENTITY_INSERT [dbo].[CartItems] ON 

INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2410, 2184, 2115, N'Rattan Teapot Storage', N'Dimension: Height 17cm x Width 12cm ', N'item', N'AUD', 1, 75, 49, 49, N'/images/product_images/Tea8586576811414898018IMG_3080.jpg', NULL, CAST(N'2018-12-06T23:41:32.570' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2434, 2199, 2111, N'Small Fruit Tray', N'Dimension (inch): Height 2.36'' x Width 11.81'' x Length 11.81''', N'item', N'AUD', 1, 32, 25, 25, N'/images/product_images/LargeFruitTray8586576819666303991IMG_3086.jpg', NULL, CAST(N'2018-12-10T11:16:08.803' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2459, 2207, 2158, N'Three Colors Tray', N'Dimension (inch): Height 2.76'' x Width 12.99'' x Length 12.99''', N'item', N'AUD', 1, 38.7, 38.7, 38.7, N'\images\app\default_product.png', NULL, CAST(N'2018-12-15T12:41:33.237' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2460, 2207, 2141, N'Navy Star Tray', N'Dimension (inch): Height 2.76'' x Width 14.57'' x Length 14.57''', N'item', N'AUD', 1, 40.5, 40.5, 40.5, N'/images/product_images/StarOrangeTray8586576157878244967IMG_3100.jpg', NULL, CAST(N'2018-12-15T12:41:44.670' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2461, 2207, 2149, N'Cherry Tray', N'Dimension: Height 7cm x Width 35cm ', N'item', N'AUD', 1, 40.15, 40.15, 40.15, N'/images/product_images/StrikeTray8586576151048579534IMG_3085.jpg', NULL, CAST(N'2018-12-15T12:41:54.660' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2462, 2207, 2159, N'Turquoise Star Tray', N'Dimension (inch): Height 2.76'' x Width 14.17'' x Length 14.17''', N'item', N'AUD', 1, 40.5, 40.5, 40.5, N'\images\app\default_product.png', NULL, CAST(N'2018-12-15T12:42:06.713' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2463, 2207, 2117, N'Lamda Tray', N'Dimension (inch): Height 2.76'' x Width 14.57'' x Length 14.57''', N'item', N'AUD', 2, 40.5, 40.5, 81, N'/images/product_images/LamdaTray8586576439520085474IMG_3101.jpg', NULL, CAST(N'2018-12-15T12:42:18.870' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2464, 2207, 2116, N'Lala Tray', N'Dimension: Height 9cm x Width 33cm ', N'item', N'AUD', 1, 36.1, 36.1, 36.1, N'/images/product_images/LalaTray8586576810427976630IMG_3098.jpg', NULL, CAST(N'2018-12-15T12:42:32.877' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2465, 2207, 2165, N'Small Rainbow Tray', N'Dimension (inch): Height 2.36'' x Width 11.02'' x Length 11.02''', N'item', N'AUD', 1, 25.2, 25.2, 25.2, N'\images\app\default_product.png', NULL, CAST(N'2018-12-15T12:42:44.100' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2466, 2207, 2166, N'Small Black & White Tray', N'Dimension (inch): Height 2.36'' x Width 11.02'' x Length 11.02''', N'item', N'AUD', 1, 25.2, 25.2, 25.2, N'\images\app\default_product.png', NULL, CAST(N'2018-12-15T12:42:54.223' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2467, 2207, 2123, N'Apple Tray', N'Dimension (inch): Height 3.54'' x Width 12.99'' x Length 12.99''', N'item', N'AUD', 1, 36.42, 36.42, 36.42, N'/images/product_images/AppleBowl8586576186780308133IMG_3111.jpg', NULL, CAST(N'2018-12-15T12:43:06.167' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2468, 2207, 2113, N'Appo Tray', N'Dimension: Height 7cm x Width 26cm ', N'item', N'AUD', 1, 26.46, 26.46, 26.46, N'/images/product_images/AppleBasket8586576815657275292IMG_3094.jpg', NULL, CAST(N'2018-12-15T12:43:23.333' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2469, 2207, 2153, N'Star Tray', N'Dimension (inch): Height 2.76'' x Width 13.78'' x Length 13.78''', N'item', N'AUD', 1, 43.2, 43.2, 43.2, N'/images/product_images/HomeStarTray8586575970495097819IMG_3129.jpg', NULL, CAST(N'2018-12-15T12:47:26.607' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2470, 2208, 2155, N'Three Colors Bowl', N'<p>Dimension (inch): Height 3.54&#39; x Width 13.39&#39; x Length 13.39&#39;<br />
Dimension (cm): Height 9cm x Width 34cm x Length 34cm<br />
Materials: Seagrass, Plastic<br />
Colors: Turquoise, Lime</p>
', N'item', N'AUD', 1, 40.5, 24, 24, N'/images/product_images/ThreeColorsBowl8586575967937113912IMG_3097.jpg', NULL, CAST(N'2018-12-16T07:27:29.650' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2471, 2208, 2098, N'Light Pink Tea Plate', N'Dimension: Height 1m x Width 15cm ', N'item', N'AUD', 1, 6, 5, 5, N'/images/product_images/IMG_3145.jpg', NULL, CAST(N'2018-12-16T07:29:44.660' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2472, 2208, 2095, N'Green Tea Plate', N'Dimension: Height 1m x Width 15cm ', N'item', N'AUD', 1, 6, 5, 5, N'/images/product_images/IMG_3142.jpg', NULL, CAST(N'2018-12-16T07:30:17.927' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2473, 2208, 2094, N'Turquoise Tea Plate', N'Dimension: Height 1m x Width 15cm ', N'item', N'AUD', 1, 6, 5, 5, N'/images/product_images/IMG_3141.jpg', NULL, CAST(N'2018-12-16T07:30:26.863' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2475, 2208, 2091, N'Strike Peacock Tea Plate', N'Dimension: Height 1m x Width 15cm ', N'item', N'AUD', 1, 6, 5, 5, N'/images/product_images/IMG_3138.jpg', NULL, CAST(N'2018-12-16T07:30:47.563' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2476, 2208, 2090, N'Blue Tea Plate', N'Dimension: Height 1m x Width 15cm ', N'item', N'AUD', 1, 6, 5, 5, N'/images/product_images/IMG_3147.jpg', NULL, CAST(N'2018-12-16T07:32:23.700' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[CartItems] ([Id], [CartId], [ProductId], [Name], [Description], [Unit], [UnitOfPrice], [Quantity], [OriginalPrice], [DiscountPrice], [FinalPrice], [PrimaryImage], [Note], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2477, 2208, 2093, N'Turquoise Black Tea Plate', N'Dimension: Height 1m x Width 15cm ', N'item', N'AUD', 1, 6, 5, 5, N'/images/product_images/IMG_3140.jpg', NULL, CAST(N'2018-12-16T07:37:58.767' AS DateTime), N'System', NULL, NULL)
SET IDENTITY_INSERT [dbo].[CartItems] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description], [PrefixForProductCode], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (1024, N'Kitchen Utensils', N'Kitchen Utensils', N'KU', 1, N'System', CAST(N'2018-07-06T23:40:58.400' AS DateTime), N'admin', CAST(N'2018-12-04T14:55:03.563' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PrefixForProductCode], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (1025, N'Home Decor Objects', N'Home Decor Objects', N'HDO', 1, N'System', CAST(N'2018-07-06T23:41:10.803' AS DateTime), N'admin', CAST(N'2018-12-04T14:54:57.297' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PrefixForProductCode], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (1026, N'Storage Bins & Baskets', N'Storage Bins & Baskets', N'SBB', 1, N'System', CAST(N'2018-07-06T23:41:29.920' AS DateTime), N'admin', CAST(N'2018-12-04T14:54:49.587' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PrefixForProductCode], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (1029, N'Baconta Embroidery', N'Baconta Embroidery', N'BE', 1, N'System', CAST(N'2018-07-06T23:42:08.490' AS DateTime), N'admin', CAST(N'2018-12-04T14:55:10.483' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PrefixForProductCode], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (1033, N'System', N'System', N'SYS', 0, N'System', CAST(N'2012-12-12T00:00:00.000' AS DateTime), N'admin', CAST(N'2018-12-04T15:13:12.310' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PrefixForProductCode], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (1034, N'Tea Plate', N'Tea Plate', N'TP', 1, N'admin', CAST(N'2018-11-26T23:37:39.487' AS DateTime), N'admin', CAST(N'2018-12-04T14:54:17.170' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PrefixForProductCode], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (1035, N'Sale', N'On sale products', N'SAL', 1, N'admin', CAST(N'2018-12-19T12:45:43.920' AS DateTime), N'admin', CAST(N'2018-12-19T13:22:01.550' AS DateTime))
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [CartId], [UserId], [FirstName], [LastName], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [DeliveryFirstName], [DeliveryLastName], [DeliveryCompanyName], [DeliveryAddress], [DeliverySuburb], [DeliveryPostcode], [DeliveryState], [DeliveryCountry], [Note], [ConfirmedStatus], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [CompletedAt], [TrackingNumber], [ShippingMethod]) VALUES (2100, 2184, 0, N'Duc', N'Duong', NULL, N'Unit 2307/11 Rose Lane', N'Melbourne', N'3000', N'Victoria', N'Australia', N'up209d@gmail.com', N'0451872009', N'Duc', N'Duong', NULL, N'Unit 2307/11 Rose Lane', N'Melbourne', N'3000', N'Victoria', N'Australia', NULL, N'SUCCEED & CONFIRMED', CAST(N'2018-12-06T23:50:26.713' AS DateTime), N'System', CAST(N'2019-01-22T07:18:44.393' AS DateTime), N'admin', CAST(N'2018-12-07T00:05:35.110' AS DateTime), N'Ship on 12/12/2018', N'HOME_MAIL')
INSERT [dbo].[Orders] ([Id], [CartId], [UserId], [FirstName], [LastName], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [DeliveryFirstName], [DeliveryLastName], [DeliveryCompanyName], [DeliveryAddress], [DeliverySuburb], [DeliveryPostcode], [DeliveryState], [DeliveryCountry], [Note], [ConfirmedStatus], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [CompletedAt], [TrackingNumber], [ShippingMethod]) VALUES (2111, 2199, 0, N'Quynh', N'Nguyen', NULL, N'131 Ridgeway parade', N'Sunshine West ', N'3020', N'Victoria', N'Australia', N'quynhnguyen1904@gmail.com', N'0425190486', N'Quynh', N'Nguyen', NULL, N'131 Ridgeway parade', N'Sunshine West ', N'3020', N'Victoria', N'Australia', NULL, N'SUCCEED & CONFIRMED', CAST(N'2018-12-10T11:19:55.573' AS DateTime), N'System', CAST(N'2019-01-09T13:19:26.707' AS DateTime), N'admin', CAST(N'2019-01-09T13:19:12.037' AS DateTime), N'Ship on 13/12/2018', N'HOME_MAIL')
INSERT [dbo].[Orders] ([Id], [CartId], [UserId], [FirstName], [LastName], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [DeliveryFirstName], [DeliveryLastName], [DeliveryCompanyName], [DeliveryAddress], [DeliverySuburb], [DeliveryPostcode], [DeliveryState], [DeliveryCountry], [Note], [ConfirmedStatus], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [CompletedAt], [TrackingNumber], [ShippingMethod]) VALUES (2114, 2207, 0, N'Tam', N'Nguyen', NULL, N'23 Tower Road', N'Weeribee', N'3030', N'Victoria', N'Australia', N'nguyentammb@gmail.com', N'0408615360', N'Tam', N'Nguyen', NULL, N'23 Tower Road', N'Weeribee', N'3030', N'Victoria', N'Australia', N'Discount 30% for Products > $30 - Discount 20% for Products <= $30. Total Discount: $311', N'SUCCEED & CONFIRMED', CAST(N'2018-12-15T12:45:47.600' AS DateTime), N'System', CAST(N'2019-01-22T06:06:42.757' AS DateTime), N'admin', CAST(N'2018-12-15T12:48:11.320' AS DateTime), N'Shipped in 15/12/2018', N'HOME_MAIL')
INSERT [dbo].[Orders] ([Id], [CartId], [UserId], [FirstName], [LastName], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [DeliveryFirstName], [DeliveryLastName], [DeliveryCompanyName], [DeliveryAddress], [DeliverySuburb], [DeliveryPostcode], [DeliveryState], [DeliveryCountry], [Note], [ConfirmedStatus], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [CompletedAt], [TrackingNumber], [ShippingMethod]) VALUES (2115, 2208, 0, N'Lai Hoang Thanh', N'Nguyen', NULL, N'1 Coward st', N'Footscray', N'3011', N'Victoria', N'Australia', N'thanhnlh@hotmail.com', N'0404 225 243', N'Lai Hoang Thanh', N'Nguyen', NULL, N'1 Coward st', N'Footscray', N'3011', N'Victoria', N'Australia', NULL, N'SUCCEED & CONFIRMED', CAST(N'2018-12-16T07:42:06.420' AS DateTime), N'System', CAST(N'2019-01-20T10:19:07.847' AS DateTime), N'admin', CAST(N'2019-01-17T13:31:17.150' AS DateTime), N'Shipped on 20/01/2019', N'CLICK_AND_COLLECT')
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([Id], [OrderId], [Method], [Amount], [Success], [IsFull], [Reason], [Detail], [IPAddress], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2231, 2100, N'PayPal', 24.5, 1, 1, N'approved', N'Payer Id: 7TXMG9W6SMLD2, Guid: 20969', N'203.206.131.36:64658', CAST(N'2018-12-07T00:05:10.253' AS DateTime), N'System', CAST(N'2018-12-07T00:05:35.110' AS DateTime), N'System')
INSERT [dbo].[Payments] ([Id], [OrderId], [Method], [Amount], [Success], [IsFull], [Reason], [Detail], [IPAddress], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2245, 2111, N'PayPal', 21.25, 0, 0, NULL, NULL, N'1.152.109.13:20439  ', CAST(N'2018-12-10T11:19:58.920' AS DateTime), N'System', NULL, NULL)
INSERT [dbo].[Payments] ([Id], [OrderId], [Method], [Amount], [Success], [IsFull], [Reason], [Detail], [IPAddress], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2246, 2115, N'PayPal', 45.9, 1, 1, N'approved', N'Payer Id: 66BPSDCLXFA8J, Guid: 90611', N'49.3.224.9:54635    ', CAST(N'2018-12-16T07:42:11.247' AS DateTime), N'System', CAST(N'2018-12-16T07:44:28.560' AS DateTime), N'System')
SET IDENTITY_INSERT [dbo].[Payments] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2090, 1034, NULL, N'TP101', N'1', N'TP101.1', N'Strike Light Turquoise Tea Plate', N'<p>Dimension: Height 1m x Width 15cm</p>
', 1, 1, 15, 15, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3137.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:40:46.493' AS DateTime), N'admin', CAST(N'2019-01-20T10:12:52.950' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2090).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2091, 1034, NULL, N'TP102', N'1', N'TP102.1', N'Strike Peacock Tea Plate', N'Dimension: Height 1m x Width 15cm ', 1, 1, 15, 15, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3138.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:41:19.933' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2091).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2092, 1034, NULL, N'TP103', N'1', N'TP103.1', N'Strike Pink Tea Plate', N'Dimension: Height 1m x Width 15cm ', 1, 1, 15, 15, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3139.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:41:39.580' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2092).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2093, 1034, NULL, N'TP104', N'1', N'TP104.1', N'Turquoise Black Tea Plate', N'Dimension: Height 1m x Width 15cm ', 1, 1, 15, 15, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3140.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:42:04.213' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2093).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2094, 1034, NULL, N'TP105', N'1', N'TP105.1', N'Turquoise Tea Plate', N'<p>Dimension: Height 1m x Width 15cm</p>
', 1, 1, 15, 15, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3141.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:43:06.740' AS DateTime), N'admin', CAST(N'2019-01-20T10:13:21.673' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2094).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2095, 1034, NULL, N'TP106', N'1', N'TP106.1', N'Green Tea Plate', N'<p>Dimension: Height 1m x Width 15cm</p>
', 1, 1, 15, 15, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3142.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:43:42.440' AS DateTime), N'admin', CAST(N'2019-01-20T10:13:27.623' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2095).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2096, 1034, NULL, N'TP107', N'1', N'TP107.1', N'Orange Tea Plate', N'<p>Dimension (inch): Height 0.39&#39; x Width 5.91&#39; x Length 5.91&#39;</p>
', 1, 1, 15, 15, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3143.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:44:16.353' AS DateTime), N'admin', CAST(N'2019-01-20T10:11:53.823' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2096).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2097, 1034, NULL, N'TP108', N'1', N'TP108.1', N'Navy Tea Plate', N'<p>Dimension: Height 1m x Width 15cm</p>
', 1, 1, 15, 15, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3144.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:44:53.403' AS DateTime), N'admin', CAST(N'2019-01-20T10:11:20.873' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2097).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2098, 1034, NULL, N'TP109', N'1', N'TP109.1', N'Light Pink Tea Plate', N'<p>Dimension: Height 1m x Width 15cm</p>
', 1, 1, 15, 15, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3145.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:45:15.680' AS DateTime), N'admin', CAST(N'2019-01-20T10:13:32.447' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2098).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2099, 1034, NULL, N'TP110', N'1', N'TP110.1', N'Pink Tea Plate', N'<p>Dimension: Height 1m x Width 15cm</p>
', 1, 1, 15, 15, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3146.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:45:38.280' AS DateTime), N'admin', CAST(N'2019-01-20T10:11:42.877' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2099).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2100, 1034, NULL, N'TP111', N'1', N'TP111.1', N'Blue Tea Plate', N'<p>Dimension: Height 1m x Width 15cm</p>
', 1, 1, 15, 15, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 2.5, 6, 6, 0, 0, 0, 0, N'/images/product_images/IMG_3147.jpg', N'/images/product_images/IMG_3135.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-11-26T23:46:04.133' AS DateTime), N'admin', CAST(N'2019-01-20T10:10:28.067' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2100).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2101, 1025, N'HL7858', N'HDO1022', N'Red', N'HDO1022.Red', N'Red Star Tray', N'Dimension: Height 7cm x Width 37cm ', 0.4, 7, 37, 37, NULL, N'LowStock', 2, 1, N'item', N'AUD', 5, 7.5, 45, 45, 0, 0, 0, 0, N'/images/product_images/RedStarTray8586577153641721156IMG_3099.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T13:45:21.480' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, N'\images\qr_code\Product(2101).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2102, 1025, N'HL7876', N'HDO1002', N'Black', N'HDO1002.Black', N'Tara Vase', N'Dimension (inch): Height 21.65'' x Width 9.84'' x Length 9.84''', 0.6, 55, 25, 25, NULL, N'LowStock', 1, 1, N'item', N'AUD', 9, 13.5, 67.5, 67.5, 0, 0, 0, 0, N'/images/product_images/TaraVase8586577151858263913IMG_3050.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 5, 5, CAST(N'2018-12-04T13:48:19.763' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2102).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2104, 1025, N'HL7878', N'HDO1004', N'Black', N'HDO1004.Black', N'Long Black Vase', N'Dimension (inch): Height 25.59'' x Width 7.87'' x Length 7.87''', 0.7, 65, 20, 20, NULL, N'LowStock', 1, 1, N'item', N'AUD', 10.55, 15.83, 94.98, 94.98, 0, 0, 0, 0, N'/images/product_images/LongBlackVase8586577135363948485IMG_3055.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T14:15:49.097' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2104).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2105, 1026, N'HL7888', N'HDO1005', N'Black', N'HDO1005.Black', N'Black Arrow Basket', N'Dimension (inch): Height 5.12'' x Width 6.69'' x Length 11.02''', 0.5, 13, 17, 28, NULL, N'LowStock', 1, 1, N'item', N'AUD', 3.85, 5.78, 34.68, 34.68, 0, 0, 0, 0, N'/images/product_images/TintinBasket8586577125830773707IMG_3089.jpg', N'/images/product_images/TintinBasket8586577125830713871IMG_3088.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T14:31:42.413' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2105).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2106, 1026, N'HL5123', N'SBB1001', N'Colorful', N'SBB1001.Colorful', N'Clothing Storage/ Laundry Storage/ Toy Storage', N'<p>Dimension (inch): Height 29.92&#39; x Width 17.72&#39; x Length 17.72&#39;<br />
Dimension (cm): Height 76cm x Width 45cm x Length 45cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Pink, Green</p>
', 1, 76, 45, 45, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 43.09, 159, 159, 0, 0, 0, 0, N'/images/product_images/ClothingStorage8586577123470912143IMG_3051.jpg', N'/images/product_images/ClothingStorage8586577123470832371IMG_3052.jpg', N'/images/product_images/ClothingStorage8586577123470692738IMG_3053.jpg', NULL, NULL, NULL, 1.39, NULL, NULL, CAST(N'2018-12-04T14:35:38.417' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Pink, Green', N'\images\qr_code\Product(2106).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2107, 1033, NULL, N'SYS1001', N'0', N'SYS1001.0', N'EVoucher Template', N'HouseHomeLove EVoucher', 0, 0, 0, 0, N'Don''t delete this template. It''s using for EVoucher payment.', N'OutOfStock', -2, 0, N'voucher', N'AUD', NULL, 0, 0, 0, 0, 0, 0, 0, N'\images\app\AdsIcon28586667577810905090voucher2.svg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-12-04T15:15:30.730' AS DateTime), N'admin', CAST(N'2018-12-04T17:09:10.530' AS DateTime), N'admin', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2108, 1026, N'HL7892', N'SBB1002', N'Yellow', N'SBB1002.Yellow', N'Poky Basket', N'<p>Dimension (inch): Height 5.12&#39; x Width 6.30&#39; x Length 10.24&#39;<br />
Dimension (cm): Height 13cm x Width 16cm x Length 26cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Brown</p>
', 0.4, 13, 16, 26, NULL, N'LowStock', 1, 1, N'item', N'AUD', 3.5, 5.25, 31.5, 31.5, 0, 0, 0, 0, N'/images/product_images/PokeBasket8586576833044310766IMG_3102.jpg', N'/images/product_images/PokeBasket8586576833044201098IMG_3103.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T22:39:41.090' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, White, Brown', N'\images\qr_code\Product(2108).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2109, 1024, N'HL7896', N'KU1001', N'Yellow', N'KU1001.Yellow', N'Oni Storage', NULL, 0.25, 7, 21, 21, NULL, N'LowStock', 1, 1, N'item', N'AUD', 2.12, 3.18, 19.1, 19.1, 0, 0, 0, 0, N'/images/product_images/OnionStorage8586576825245247066IMG_3090.jpg', N'/images/product_images/OnionStorage8586576825245127361IMG_3091.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T22:52:40.973' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Pastic', N'Seagrass, White', N'\images\qr_code\Product(2109).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2110, 1026, N'HL7895', N'SBB1013', N'Turquoise', N'SBB1013.Turquoise', N'Sky Basket', N'Dimension (inch): Height 5.51'' x Width 9.45'' x Length 14.57''', 0.5, 14, 24, 37, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.86, 7.29, 43.75, 43.75, 0, 0, 0, 0, N'/images/product_images/FruitBasket8586576823651606994IMG_3082.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T22:55:20.330' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2110).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2111, 1024, N'HL7893S', N'KU1003', N'Black', N'KU1003.Black', N'Small Fruit Tray', N'Dimension (inch): Height 2.36'' x Width 11.81'' x Length 11.81''', 0.4, 6, 30, 30, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 3.91, 5.87, 35.22, 35.22, 0, 0, 0, 0, N'/images/product_images/LargeFruitTray8586576819666303991IMG_3086.jpg', N'/images/product_images/LargeFruitTray8586576819666194281IMG_3087.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 6, CAST(N'2018-12-04T23:01:58.867' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2111).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2112, 1024, N'HL4000I/3', N'KU1004', N'Yellow', N'KU1004.Yellow', N'White Tray', N'Dimension (inch): Height 2.36'' x Width 9.06'' x Length 9.06''', 0.3, 6, 23, 23, NULL, N'LowStock', 1, 1, N'item', N'AUD', 2.25, 3.38, 20.3, 20.3, 0, 0, 0, 0, N'/images/product_images/AppleTray8586576817041706015IMG_3096.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T23:06:21.327' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2112).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2113, 1024, N'HL7885', N'KU1005', N'Colorful', N'KU1005.Colorful', N'Appo Tray', N'Dimension: Height 7cm x Width 26cm ', 0.4, 7, 26, 26, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 2.52, 3.78, 26.46, 26.46, 0, 0, 0, 0, N'/images/product_images/AppleBasket8586576815657275292IMG_3094.jpg', N'/images/product_images/AppleBasket8586576815657085795IMG_3095.jpg', NULL, NULL, NULL, NULL, 1.5, 7, 6, CAST(N'2018-12-04T23:08:39.780' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2113).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2114, 1024, N'HL3918', N'KU1006', N'Strike', N'KU1006.Strike', N'Fruit Camping Basket', N'<p>Dimension (inch): Height 11.81&#39; x Width 11.81&#39; x Length 11.81&#39;<br />
Dimension (cm): Height 30cm x Width 30cm x Length 30cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Black</p>
', 0.5, 30, 30, 30, NULL, N'LowStock', 1, 1, N'item', N'AUD', 8.01, 12.02, 72.2, 72.2, 0, 0, 0, 0, N'/images/product_images/FruitCampingBasket8586576813997931978IMG_3079.jpg', N'/images/product_images/FruitCampingBasket8586576813997782406IMG_3078.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T23:11:25.713' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, White, Black', N'\images\qr_code\Product(2114).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2115, 1025, N'HL5346', N'HDO1006', N'Orange', N'HDO1006.Orange', N'Rattan Teapot Storage', N'Dimension: Height 17cm x Width 12cm ', 0.2, 17, 12, 12, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', NULL, 15, 75, 75, 0, 0, 0, 0, N'/images/product_images/Tea8586576811414898018IMG_3080.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-12-04T23:15:43.997' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2115).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2116, 1024, N'HL7863', N'KU1007', N'Yellow', N'KU1007.Yellow', N'Lala Tray', N'Dimension: Height 9cm x Width 33cm ', 0.2, 9, 33, 33, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 4.01, 6.01, 36.1, 36.1, 0, 0, 0, 0, N'/images/product_images/LalaTray8586576810427976630IMG_3098.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-04T23:17:22.690' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2116).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2117, 1025, N'HL7860', N'HDO1007', N'Strike', N'HDO1007.Strike', N'Lamda Tray', N'Dimension (inch): Height 2.76'' x Width 14.57'' x Length 14.57''', 0.3, 7, 37, 37, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 4.5, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'/images/product_images/LamdaTray8586576439520085474IMG_3101.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T09:35:33.503' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, N'\images\qr_code\Product(2117).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2118, 1025, N'HL7442S', N'HDO1008', N'Colorful', N'HDO1008.Colorful', N'Candy Box', N'Dimension (inch): Height 3.15'' x Width 11.42'' x Length 11.42''', 0.2, 8, 29, 29, NULL, N'LowStock', 1, 1, N'item', N'AUD', 2.25, 3.38, 20.3, 20.3, 0, 0, 0, 0, N'/images/product_images/CandyBox8586576438194392615IMG_3092.jpg', N'/images/product_images/CandyBox8586576438194302871IMG_3093.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T09:37:46.053' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2118).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2119, 1025, N'HL5802', N'HDO1009', N'Yellow', N'HDO1009.Yellow', N'Rattan Storage', N'<p>Dimension (inch): Height 7.48&#39; x Width 5.51&#39; x Length 5.51&#39;<br />
Dimension (cm): Height 19cm x Width 14cm x Length 14cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Yellow</p>
', 0.2, 19, 14, 14, NULL, N'LowStock', 1, 1, N'item', N'AUD', 10, 15, 75, 75, 0, 0, 0, 0, N'/images/product_images/BigTeapotStorage8586576426775213880IMG_3116.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 5, 4, CAST(N'2018-12-05T09:56:47.990' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Yellow', N'\images\qr_code\Product(2119).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2120, 1026, N'HL7871', N'SBB1003', N'Blue', N'SBB1003.Blue', N'Navy Storage', N'Dimension (inch): Height 10.63'' x Width 9.84'' x Length 9.84''', 0.3, 27, 25, 25, NULL, N'LowStock', 1, 1, N'item', N'AUD', 5.95, 8.93, 53.6, 53.6, 0, 0, 0, 0, N'/images/product_images/TinyBin8586576419279041871IMG_3076.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T10:09:17.757' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2120).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2121, 1026, N'HL6190', N'SBB1004', N'Colorful', N'SBB1004.Colorful', N'Laundry Basket', N'Dimension: Height 40cm x Width 33cm ', 0.5, 40, 33, 33, NULL, N'LowStock', 1, 1, N'item', N'AUD', 11.35, 17.02, 93.6, 93.6, 0, 0, 0, 0, N'/images/product_images/ClothingBin8586576414869080527IMG_3064.jpg', N'/images/product_images/ClothingBin8586576414868980782IMG_3062.jpg', NULL, NULL, NULL, NULL, 1.5, 5.5, 4.5, CAST(N'2018-12-05T10:16:38.593' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2121).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2122, 1026, N'HL7873', N'SBB1005', N'Strike', N'SBB1005.Strike', N'Laundry Basket With Lid', N'<p>Dimension (inch): Height 15.75&#39; x Width 12.60&#39; x Length 12.60&#39;<br />
Dimension (cm): Height 40cm x Width 32cm x Length 32cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Black, White</p>
', 0.5, 40, 32, 32, NULL, N'LowStock', 1, 1, N'item', N'AUD', 7.65, 11.48, 68.9, 68.9, 0, 0, 0, 0, N'/images/product_images/ClothingBin8586576413546448902IMG_3066.jpg', N'/images/product_images/ClothingBin8586576413546309121IMG_3065.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T10:18:50.870' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Black, White', N'\images\qr_code\Product(2122).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2123, 1024, N'HL7857', N'KU1008', N'Black', N'KU1008.Black', N'Apple Tray', N'Dimension (inch): Height 3.54'' x Width 12.99'' x Length 12.99''', 0.2, 9, 33, 33, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 4.05, 6.07, 36.42, 36.42, 0, 0, 0, 0, N'/images/product_images/AppleBowl8586576186780308133IMG_3111.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:36:47.563' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2123).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2124, 1024, N'HL4093', N'KU1009', N'Green', N'KU1009.Green', N'Teharan Tray', N'Dimension (inch): Height 3.94'' x Width 13.39'' x Length 15.75''', 0.3, 10, 34, 40, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.05, 6.07, 36.42, 36.42, 0, 0, 0, 0, N'/images/product_images/TeharanTray8586576185771016064IMG_3105.jpg', N'/images/product_images/TeharanTray8586576185770906331IMG_3104.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:38:28.397' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2124).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2125, 1024, N'HL66264', N'KU1010', N'Black', N'KU1010.Black', N'Navy Big Bowl', N'Dimension (inch): Height 3.54'' x Width 13.39'' x Length 13.39''', 0.3, 9, 34, 34, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.28, 6.42, 38.5, 38.5, 0, 0, 0, 0, N'/images/product_images/BlackBigBowl8586576184894614092IMG_3108.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:39:56.023' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2125).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2126, 1025, N'HL7899', N'HDO1010', N'Yellow', N'HDO1010.Yellow', N'Small Rectangle Tray', N'<p>Dimension (inch): Height 2.36&#39; x Width 10.24&#39; x Length 14.57&#39;<br />
Dimension (cm): Height 6cm x Width 26cm x Length 37cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Black</p>
', 0.3, 6, 26, 37, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.4, 6.6, 39.6, 39.6, 0, 0, 0, 0, N'/images/product_images/SmallRectangleTray8586576183911467416IMG_3113.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:41:34.340' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Black', N'\images\qr_code\Product(2126).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2127, 1025, N'HL7884', N'HDO1011', N'Colorful', N'HDO1011.Colorful', N'Small Colorful Bowl', N'Dimension (inch): Height 2.76'' x Width 9.45'' x Length 9.45''', 0.2, 7, 24, 24, NULL, N'LowStock', 1, 1, N'item', N'AUD', 2.7, 4.05, 24.3, 24.3, 0, 0, 0, 0, N'/images/product_images/SmallColorfulBowl8586576182664913605IMG_3112.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:43:38.997' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, N'\images\qr_code\Product(2127).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2128, 1026, N'HL7902', N'SBB1006', N'Orange', N'SBB1006.Orange', N'Pink Strike Plant Pot', N'<p>Dimension (inch): Height 7.09&#39; x Width 9.84&#39; x Length 9.84&#39;<br />
Dimension (cm): Height 18cm x Width 25cm x Length 25cm<br />
Materials: Seagrass, Plastic, Nilon<br />
Colors: Seagrass, White, Pink</p>
', 0.3, 18, 25, 25, NULL, N'LowStock', 1, 1, N'item', N'AUD', 3.9, 5.85, 35.1, 35.1, 0, 0, 0, 0, N'/images/product_images/OrangeBasket8586576179665088742IMG_3120.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:48:39.030' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic, Nilon', N'Seagrass, White, Pink', N'\images\qr_code\Product(2128).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2129, 1024, N'HL7886', N'KU1011', N'Green', N'KU1011.Green', N'Green Tray', N'Dimension (inch): Height 1.18'' x Width 10.24'' x Length 10.24''', 0.3, 3, 26, 26, NULL, N'LowStock', 1, 1, N'item', N'AUD', 1.5, 2.25, 13.5, 13.5, 0, 0, 0, 0, N'/images/product_images/GreenTray8586576178332604116IMG_3109.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:50:52.230' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2129).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2130, 1024, N'HL7883', N'KU1012', N'Strike', N'KU1012.Strike', N'Small Strike Bowl', N'Dimension (inch): Height 2.76'' x Width 9.84'' x Length 9.84''', 0.2, 7, 25, 25, NULL, N'LowStock', 1, 1, N'item', N'AUD', 2.52, 3.78, 22.7, 22.7, 0, 0, 0, 0, N'/images/product_images/SmallStrikeBowl8586576177268682145IMG_3110.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:52:38.617' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2130).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2131, 1026, N'HL7874', N'SBB1007', N'Strike', N'SBB1007.Strike', N'Storage basket', N'<p>Dimension (inch): Height 15.75&#39; x Width 12.60&#39; x Length 12.60&#39;<br />
Dimension (cm): Height 40cm x Width 32cm x Length 32cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Gray</p>
', 0.4, 40, 32, 32, NULL, N'LowStock', 1, 1, N'item', N'AUD', 7.65, 11.48, 68.9, 68.9, 0, 0, 0, 0, N'/images/product_images/FruitCampingBasket8586576175621407638IMG_3058.jpg', N'/images/product_images/FruitCampingBasket8586576175621357869IMG_3057.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:55:23.347' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, White, Gray', N'\images\qr_code\Product(2131).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2132, 1025, N'HL7887', N'HDO1012', N'Vecni', N'HDO1012.Vecni', N'Fan Tray', N'Dimension (inch): Height 9.84'' x Width 17.32'' x Length 17.32''', 0.3, 25, 44, 44, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.95, 7.43, 44.58, 44.58, 0, 0, 0, 0, N'/images/product_images/FanTray8586576174126297526IMG_3125.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T16:57:52.857' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, N'\images\qr_code\Product(2132).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2133, 1025, N'HL6678', N'HDO1014', N'Star', N'HDO1014.Star', N'Big Star Bowl', N'<p>Dimension (inch): Height 6.30&#39; x Width 15.35&#39; x Length 15.35&#39;<br />
Dimension (cm): Height 16cm x Width 39cm x Length 39cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Black</p>
', 0.3, 16, 39, 39, NULL, N'LowStock', 1, 1, N'item', N'AUD', 5.85, 8.77, 52.6, 52.6, 0, 0, 0, 0, N'/images/product_images/StarBowl8586576172551818914IMG_3122.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:00:30.307' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, White, Black', N'\images\qr_code\Product(2133).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2134, 1026, N'HL6226S', N'SBB1009', N'Strike', N'SBB1009.Strike', N'Mango Basket', N'<p>Dimension (inch): Height 7.87&#39; x Width 12.20&#39; x Length 12.20&#39;<br />
Dimension (cm): Height 20cm x Width 31cm x Length 31cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Black</p>
', 0.3, 20, 31, 31, NULL, N'LowStock', 1, 1, N'item', N'AUD', 7.2, 10.8, 64.8, 64.8, 0, 0, 0, 0, N'/images/product_images/MangoBasket8586576169334660660IMG_3075.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:05:52.023' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Black', N'\images\qr_code\Product(2134).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2135, 1026, N'HL7868', N'SBB1010', N'Yellow', N'SBB1010.Yellow', N'Tin Tin Basket', N'<p>Dimension (inch): Height 9.84&#39; x Width 11.81&#39; x Length 11.81&#39;<br />
Dimension (cm): Height 25cm x Width 30cm x Length 30cm<br />
Materials: Seagrass, Rattan, Plastic<br />
Colors: Seagrass, White</p>
', 1, 25, 30, 30, NULL, N'LowStock', 1, 1, N'item', N'AUD', 8.2, 12.3, 73.8, 73.8, 0, 0, 0, 0, N'/images/product_images/TinTinBin8586576167675366317IMG_3069.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:08:37.953' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Rattan, Plastic', N'Seagrass, White', N'\images\qr_code\Product(2135).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2136, 1024, N'HL7894', N'KU1013', N'Green', N'KU1013.Green', N'Halfway Tray', N'Dimension (inch): Height 3.94'' x Width 11.42'' x Length 15.35''', 0.2, 10, 29, 39, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.5, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'/images/product_images/HalfwayTray8586576167106078427IMG_3114.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:09:34.880' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2136).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2137, 1025, N'HL7879', N'HDO1013', N'Navy', N'HDO1013.Navy', N'Navy High Vase', N'Dimension (inch): Height 22.05'' x Width 8.66'' x Length 8.66''', 0.4, 56, 22, 22, NULL, N'LowStock', 1, 1, N'item', N'AUD', 10.55, 15.83, 94.98, 94.98, 0, 0, 0, 0, N'/images/product_images/NavyHighVase8586576166017813981IMG_3054.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:11:23.703' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2137).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2138, 1024, N'HL7897', N'KU1014', N'Yellow', N'KU1014.Yellow', N'Food Covered Tray', N'<p>Dimension (inch): Height 10.24&#39; x Width 12.60&#39; x Length 12.60&#39;<br />
Dimension (cm): Height 26cm x Width 32cm x Length 32cm<br />
Materials: Seagrass, Plastic, Bamboo, Fabric<br />
Colors: Seagrass, White, Black</p>
', 0.3, 26, 32, 32, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.6, 6.9, 41.4, 41.4, 0, 0, 0, 0, N'/images/product_images/FoodCoveredTray8586576165082874558IMG_3107.jpg', N'/images/product_images/FoodCoveredTray8586576165082774820IMG_3106.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:12:57.213' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic, Bamboo, Fabric', N'Seagrass, White, Black', N'\images\qr_code\Product(2138).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2139, 1024, N'HL7889', N'KU1015', N'Yellow', N'KU1015.Yellow', N'Oval Storage', NULL, 0.3, 14, 23, 37, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.86, 7.29, 43.75, 43.75, 0, 0, 0, 0, N'/images/product_images/CatBox8586576163327623054IMG_3070.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:15:52.720' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2139).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2140, 1025, N'HL5349', N'HDO1015', N'Vecni', N'HDO1015.Vecni', N'Women Bag', N'<p>Dimension (inch): Height 6.69&#39; x Width 3.94&#39; x Length 8.86&#39;<br />
Dimension (cm): Height 17cm x Width 10cm x Length 22.5cm<br />
Materials: Rattan<br />
Colors: Rattan, Green</p>
', 0.3, 17, 10, 22.5, NULL, N'LowStock', 1, 1, N'item', N'AUD', 7.8, 11.7, 64.35, 64.35, 0, 0, 0, 0, N'/images/product_images/WomenBag8586576161969288219IMG_3072.jpg', N'/images/product_images/WomenBag8586576161969178494IMG_3071.jpg', NULL, NULL, NULL, NULL, 1.5, 5.5, 4.5, CAST(N'2018-12-05T17:18:08.567' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Rattan', N'Rattan, Green', N'\images\qr_code\Product(2140).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2141, 1025, N'HL7859', N'HDO1018', N'Star', N'HDO1018.Star', N'Navy Star Tray', N'Dimension (inch): Height 2.76'' x Width 14.57'' x Length 14.57''', 0.2, 7, 37, 37, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 4.5, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'/images/product_images/StarOrangeTray8586576157878244967IMG_3100.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 6, CAST(N'2018-12-05T17:24:57.663' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, N'\images\qr_code\Product(2141).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2142, 1026, N'HL7856', N'SBB1011', N'Strike', N'SBB1011.Strike', N'Sapa Basket', N'<p>Dimension (inch): Height 12.20&#39; x Width 13.39&#39; x Length 13.39&#39;<br />
Dimension (cm): Height 31cm x Width 34cm x Length 34cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Black, White, Grey, Mustard</p>
', 0.4, 31, 34, 34, NULL, N'LowStock', 1, 1, N'item', N'AUD', 8.05, 12.08, 72.5, 72.5, 0, 0, 0, 0, N'/images/product_images/SapaBasket8586576156407397672IMG_3060.jpg', N'/images/product_images/SapaBasket8586576156407297941IMG_3059.jpg', N'/images/product_images/SapaBasket8586576156099254301IMG_3061.jpg', NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:27:24.753' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Black, White, Grey, Mustard', N'\images\qr_code\Product(2142).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2143, 1024, N'HL7893L', N'KU1016', N'Star', N'KU1016.Star', N'Large Fruit Tray', N'Dimension (inch): Height 2.76'' x Width 5.51'' x Length 13.78''', 0.3, 7, 14, 35, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.5, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'/images/product_images/StarLineTray8586576155203332753IMG_3128.jpg', N'/images/product_images/StarLineTray8586576155203272861IMG_3127.jpg', NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:29:25.160' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2143).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2144, 1025, N'HL7882', N'HDO1016', N'Strike', N'HDO1016.Strike', N'Senegalese Flower Vase', N'Dimension (inch): Height 17.72'' x Width 12.99'' x Length 12.99''', 0.3, 45, 33, 33, NULL, N'LowStock', 1, 1, N'item', N'AUD', 10.35, 15.52, 93.2, 93.2, 0, 0, 0, 0, N'/images/product_images/TraditionalBasket8586576154430192484IMG_3056.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:30:42.467' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2144).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2145, 1025, N'HL7866', N'HDO1017', N'Red', N'HDO1017.Red', N'Red Flower Tray', N'Dimension (inch): Height 3.15'' x Width 18.11'' x Length 18.11''', 0.3, 8, 46, 46, NULL, N'LowStock', 1, 1, N'item', N'AUD', 6.75, 10.13, 55.5, 55.5, 0, 0, 0, 0, N'/images/product_images/RedFlowerTray8586576153684210470IMG_3121.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, NULL, NULL, CAST(N'2018-12-05T17:31:57.067' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, N'\images\qr_code\Product(2145).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2146, 1025, N'HL7861', N'HDO1020', N'Yellow', N'HDO1020.Yellow', N'White Flower Tray', N'Dimension (inch): Height 2.36'' x Width 16.14'' x Length 16.14''', 0.3, 6, 41, 41, NULL, N'LowStock', 1, 1, N'item', N'AUD', 4.68, 7.02, 42.12, 42.12, 0, 0, 0, 0, N'/images/product_images/YellowStarTray8586576153231884772IMG_3123.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:32:42.297' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2146).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2147, 1024, N'HL4710', N'KU1018', N'Caro', N'KU1018.Caro', N'Tea Plate Box', N'<p>Dimension (inch): Height 2.36&#39; x Width 7.87&#39; x Length 7.87&#39;<br />
Dimension (cm): Height 6cm x Width 20cm x Length 20cm<br />
Materials: Rattan<br />
Colors: Rattan</p>
', 0.3, 6, 20, 20, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 3, 18, 18, 0, 0, 0, 0, N'/images/product_images/TeaPlateBox8586576152561835626IMG_3124.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:33:49.303' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Rattan', N'Rattan', N'\images\qr_code\Product(2147).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2148, 1024, N'HL7890', N'KU1019', N'Bat', N'KU1019.Bat', N'Bat Tray', N'Dimension: Height 6cm x Width 35cm ', 0.3, 6, 35, 35, NULL, N'LowStock', 1, 1, N'item', N'AUD', 5, 7.5, 45, 45, 0, 0, 0, 0, N'/images/product_images/BatTray8586576151701274767IMG_3126.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:35:15.373' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, N'\images\qr_code\Product(2148).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2149, 1025, N'HL7867', N'HDO1024', N'Strike', N'HDO1024.Strike', N'Cherry Tray', N'Dimension: Height 7cm x Width 35cm ', 0.3, 7, 35, 35, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 4.46, 6.69, 40.15, 40.15, 0, 0, 0, 0, N'/images/product_images/StrikeTray8586576151048579534IMG_3085.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T17:36:20.640' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, N'\images\qr_code\Product(2149).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2150, 1024, NULL, N'KU1020', N'Strike', N'KU1020.Strike', N'Pen Storage', NULL, 0.2, 15, 7, 7, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 0, 24, 24, 0, 0, 0, 0, N'/images/product_images/PenBox8586576150571211651IMG_3117.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-12-05T17:37:08.367' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, 1026, NULL, NULL, N'\images\qr_code\Product(2150).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2151, 1026, N'HL7903', N'SBB1012', N'Strike', N'SBB1012.Strike', N'Seagrass Storage ', N'<p>Dimension (inch): Height 6.30&#39; x Width 7.09&#39; x Length 7.09&#39;<br />
Dimension (cm): Height 16cm x Width 18cm x Length 18cm<br />
Materials: Seegrass, Plastic<br />
Colors: Black, Seagrass</p>
', 0.3, 16, 18, 18, NULL, N'LowStock', 1, 1, N'item', N'AUD', 2.88, 4.32, 25.9, 25.9, 0, 0, 0, 0, N'/images/product_images/SmallBin8586575972600357589IMG_3118.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T22:33:45.523' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seegrass, Plastic', N'Black, Seagrass', N'\images\qr_code\Product(2151).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2153, 1025, NULL, N'HDO1025', N'Black', N'HDO1025.Black', N'Star Tray', N'Dimension (inch): Height 2.76'' x Width 13.78'' x Length 13.78''', 0.3, 7, 35, 35, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 4.8, 7.2, 43.2, 43.2, 0, 0, 0, 0, N'/images/product_images/HomeStarTray8586575970495097819IMG_3129.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-05T22:37:15.977' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, N'\images\qr_code\Product(2153).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2154, 1025, NULL, N'HDO1021', N'Black', N'HDO1021.Black', N'Flower Vase', N'<p>Dimension (inch): Height 17.72&#39; x Width 7.09&#39; x Length 7.09&#39;</p>
', 0.3, 45, 18, 18, NULL, N'LowStock', 1, 1, N'item', N'AUD', NULL, 0, 49, 49, 0, 0, 0, 0, N'/images/product_images/FlowerBottle8586575969232554302IMG_3115.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-12-05T22:39:22.230' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, NULL, NULL, N'\images\qr_code\Product(2154).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2155, 1024, NULL, N'KU1021', N'Colorful', N'KU1021.Colorful', N'Three Colors Bowl', N'<p>Dimension (inch): Height 3.54&#39; x Width 13.39&#39; x Length 13.39&#39;<br />
Dimension (cm): Height 9cm x Width 34cm x Length 34cm<br />
Materials: Seagrass, Plastic<br />
Colors: Turquoise, Lime</p>
', 0.3, 9, 34, 34, NULL, N'OutOfStock', 0, 1, N'item', N'AUD', 4.5, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'/images/product_images/ThreeColorsBowl8586575967937113912IMG_3097.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 6, CAST(N'2018-12-05T22:41:31.773' AS DateTime), N'admin', CAST(N'2019-01-20T10:07:27.800' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Turquoise, Lime', N'\images\qr_code\Product(2155).png')
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2157, 1025, N'HL25', N'HDO1001', N'Strike', N'HDO1001.Strike', N'Tray', N'Dimension (inch): Height 2.76'' x Width 13.78'' x Length 13.78''', 0.3, 7, 35, 35, NULL, N'LowStock', 1, 0, N'item', N'AUD', 6.75, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T12:35:35.483' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2158, 1025, N'HL23', N'HDO1023', N'Colorful', N'HDO1023.Colorful', N'Three Colors Tray', N'Dimension (inch): Height 2.76'' x Width 12.99'' x Length 12.99''', 0.3, 7, 33, 33, NULL, N'OutOfStock', 0, 0, N'item', N'AUD', 6.45, 6.45, 38.7, 38.7, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T12:38:47.423' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2159, 1025, N'HL22', N'HDO1026', N'Turquoise', N'HDO1026.Turquoise', N'Turquoise Star Tray', N'Dimension (inch): Height 2.76'' x Width 14.17'' x Length 14.17''', 0.3, 7, 36, 36, NULL, N'OutOfStock', 0, 0, N'item', N'AUD', 6.75, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T13:05:54.867' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2160, 1024, N'HL19', N'KU1022', N'Red', N'KU1022.Red', N'Red Flower Tray Small', N'Dimension (inch): Height 3.15'' x Width 13.78'' x Length 13.78''', 0.3, 8, 35, 35, NULL, N'LowStock', 1, 0, N'item', N'AUD', 7.5, 7.5, 45, 45, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T13:13:09.287' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2161, 1025, N'HL17', N'HDO1027', N'Green', N'HDO1027.Green', N'Green Tray', N'Dimension (inch): Height 2.36'' x Width 13.78'' x Length 13.78''', 0.4, 6, 35, 35, NULL, N'LowStock', 1, 0, N'item', N'AUD', 6.75, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T13:22:11.837' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2162, 1024, N'HL07', N'KU1023', N'RedWhite', N'KU1023.RedWhite', N'Florence Tray', N'Dimension (inch): Height 2.76'' x Width 12.20'' x Length 12.20''', 0.3, 7, 31, 31, NULL, N'LowStock', 1, 0, N'item', N'AUD', 4.8, 4.8, 28.8, 28.8, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T13:24:57.343' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2163, 1025, N'HL27', N'HDO1028', N'Ridge', N'HDO1028.Ridge', N'Big Ridge Tray', N'Dimension (inch): Height 3.15'' x Width 16.14'' x Length 16.14''', 0.4, 8, 41, 41, NULL, N'LowStock', 1, 0, N'item', N'AUD', 9.75, 9.75, 58.5, 58.5, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T13:29:29.747' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2164, 1024, N'HL26', N'KU1024', N'Pink', N'KU1024.Pink', N'Pink Tray', N'Dimension (inch): Height 2.76'' x Width 14.57'' x Length 14.57''', 0.3, 7, 37, 37, NULL, N'LowStock', 1, 0, N'item', N'AUD', 7.05, 7.05, 42.3, 42.3, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T13:35:06.267' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2165, 1025, N'HL5282', N'HDO1029', N'Rainbow', N'HDO1029.Rainbow', N'Small Rainbow Tray', N'Dimension (inch): Height 2.36'' x Width 11.02'' x Length 11.02''', 0.2, 6, 28, 28, NULL, N'OutOfStock', 0, 0, N'item', N'AUD', 4.2, 4.2, 25.2, 25.2, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T13:36:59.877' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2166, 1025, NULL, N'HDO1030', N'BlackWhite', N'HDO1030.BlackWhite', N'Small Black & White Tray', N'Dimension (inch): Height 2.36'' x Width 11.02'' x Length 11.02''', 0.3, 6, 28, 28, NULL, N'OutOfStock', 0, 0, N'item', N'AUD', 4.2, 4.2, 25.2, 25.2, 0, 0, 0, 0, N'\images\app\default_product.png', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-10T13:38:47.580' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2167, 1026, N'HL04', N'SBB1014', N'Colorful', N'SBB1014.Colorful', N'Rainbow Circle Box', N'Dimension (inch): Height 7.09'' x Width 11.81'' x Length 11.81''', 0.5, 18, 30, 30, NULL, N'LowStock', 1, 0, N'item', N'AUD', 7, 10.5, 63, 63, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-19T11:00:02.610' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, 1025, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2168, 1024, N'HL12', N'KU1025', N'Pink', N'KU1025.Pink', N'Sauces Box', N'Dimension (inch): Height 4.72'' x Width 7.87'' x Length 7.87''', 0.4, 12, 20, 20, NULL, N'LowStock', 1, 0, N'item', N'AUD', 6.1, 9.15, 54.9, 54.9, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-19T11:08:03.287' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2169, 1026, N'HL05', N'SBB1015', N'Silver', N'SBB1015.Silver', N'Silver Arrow Basket', N'Dimension (inch): Height 5.91'' x Width 7.87'' x Length 11.81''', 0.5, 15, 20, 30, NULL, N'LowStock', 1, 0, N'item', N'AUD', 6, 9, 54, 54, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-19T11:25:01.027' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, 1025, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2170, 1026, N'HL08', N'SBB1016', N'Seagrass', N'SBB1016.Seagrass', N'Seagrass Square Basket', N'Dimension (inch): Height 3.54'' x Width 8.66'' x Length 8.66''', 0.25, 9, 22, 22, NULL, N'LowStock', 1, 0, N'item', N'AUD', 4.3, 6.45, 38.7, 38.7, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-19T11:33:10.740' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, 1025, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2171, 1024, N'HL14', N'KU1026', N'Black', N'KU1026.Black', N'Tera Tray', N'Dimension (inch): Height 3.15'' x Width 9.06'' x Length 9.06''', 0.2, 8, 23, 23, NULL, N'LowStock', 1, 0, N'item', N'AUD', 2.5, 3.75, 22.5, 22.5, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-19T11:37:48.843' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2172, 1025, N'HL11', N'HDO1031', N'Green', N'HDO1031.Green', N'Green Tissue Box', N'Dimension (inch): Height 5.51'' x Width 5.51'' x Length 5.51''', 0.2, 14, 14, 14, NULL, N'LowStock', 1, 0, N'item', N'AUD', 4, 6, 30, 30, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 5, 4, CAST(N'2018-12-19T12:09:42.920' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1024, NULL, NULL, NULL, NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2173, 1026, N'HL10', N'SBB1017', N'Navy', N'SBB1017.Navy', N'Circle Basket', NULL, 0.5, 12, 30, 30, NULL, N'LowStock', 1, 0, N'item', N'AUD', 5, 7.5, 45, 45, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T10:41:38.677' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, N'Seagrass, Plastic', N'Seagrass, Navy', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2174, 1024, N'HL01', N'KU1027', N'Rainbow', N'KU1027.Rainbow', N'Rainbow Bowl', N'<p>Dimension (inch): Height 5.51&#39; x Width 11.42&#39; x Length 11.42&#39;<br />
Dimension (cm): Height 14cm x Width 29cm x Length 29cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Yellow, Navy, Orange, Green, Turquoise</p>
', 0.4, 14, 29, 29, NULL, N'LowStock', 1, 0, N'item', N'AUD', 4, 6, 36, 36, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T10:48:25.497' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, 1026, N'Seagrass, Plastic', N'Seagrass, Yellow, Navy, Orange, Green, Turquoise', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2175, 1026, NULL, N'SBB1018', N'Blue', N'SBB1018.Blue', N'Blue Plan Plot', NULL, 0.4, 18, 25, 25, NULL, N'LowStock', 1, 0, N'item', N'AUD', 4, 6, 36, 36, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T10:56:53.363' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic, Nilon', N'Seagrass, White, Blue', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2176, 1026, N'HL02', N'SBB1019', N'Dual', N'SBB1019.Dual', N'Parallel Basket', N'<p>Dimension (inch): Height 9.84&#39; x Width 9.84&#39; x Length 9.84&#39;<br />
Dimension (cm): Height 25cm x Width 25cm x Length 25cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Pink, Grey</p>
', 0.4, 25, 25, 25, NULL, N'LowStock', 1, 0, N'item', N'AUD', 6.8, 10.2, 61.2, 61.2, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T11:03:38.640' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Pink, Grey', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2177, 1024, N'HL4284', N'KU1028', N'Rainbow', N'KU1028.Rainbow', N'Hani Tray', N'<p>Dimension (inch): Height 3.15&#39; x Width 7.09&#39; x Length 16.54&#39;<br />
Dimension (cm): Height 8cm x Width 18cm x Length 42cm<br />
Materials: Rattan, Plastic<br />
Colors: Red, Pink, Purple, Yellow, Navy, Blue</p>
', 0, 8, 18, 42, NULL, N'LowStock', 1, 0, N'item', N'AUD', 4.1, 6.15, 36.9, 36.9, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T11:22:15.763' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Rattan, Plastic', N'Red, Pink, Purple, Yellow, Navy, Blue', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2178, 1024, N'HL09', N'KU1029', N'Colorful', N'KU1029.Colorful', N'Hatley Bowl', N'<p>Dimension (inch): Height 3.54&#39; x Width 10.63&#39; x Length 10.63&#39;<br />
Dimension (cm): Height 9cm x Width 27cm x Length 27cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Blue, Green</p>
', 0.3, 9, 27, 27, NULL, N'LowStock', 1, 0, N'item', N'AUD', 3.8, 5.7, 34.2, 34.2, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T11:26:57.033' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, White, Blue, Green', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2179, 1025, N'HL28', N'HDO1032', N'Turquoise', N'HDO1032.Turquoise', N'Small Cheery Basket', N'<p>Dimension (inch): Height 5.12&#39; x Width 6.30&#39; x Length 10.24&#39;<br />
Dimension (cm): Height 13cm x Width 16cm x Length 26cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Brown</p>
', 0.2, 24, 16, 16, NULL, N'LowStock', 1, 0, N'item', N'AUD', 2.5, 3.75, 22.5, 22.5, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T11:31:04.900' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Turquoise', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2180, 1025, N'HL0011', N'HDO1033', N'Colorful', N'HDO1033.Colorful', N'Spin Basket', N'<p>Dimension (inch): Height 5.12&#39; x Width 6.30&#39; x Length 10.24&#39;<br />
Dimension (cm): Height 13cm x Width 16cm x Length 26cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Brown</p>
', 0.3, 27, 12, 25, NULL, N'LowStock', 1, 0, N'item', N'AUD', 11.5, 17.25, 86.25, 86.25, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 5, 4, CAST(N'2018-12-21T11:34:25.360' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, White, Turquoise, Green, Pink, Purple, Yellow', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2181, 1026, N'HL03', N'SBB1020', N'Colorful', N'SBB1020.Colorful', N'Desert Basket', N'<p>Dimension (inch): Height 8.27&#39; x Width 12.20&#39; x Length 12.20&#39;<br />
Dimension (cm): Height 21cm x Width 31cm x Length 31cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Olive, Mustard</p>
', 0.6, 21, 31, 31, NULL, N'LowStock', 1, 0, N'item', N'AUD', 7.5, 11.25, 67.5, 67.5, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T11:39:31.643' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, White, Olive, Mustard', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2182, 1026, N'HL13', N'SBB1021', N'Colorful', N'SBB1021.Colorful', N'Martin Bin', N'<p>Dimension (inch): Height 11.81&#39; x Width 11.81&#39; x Length 11.81&#39;<br />
Dimension (cm): Height 30cm x Width 30cm x Length 30cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Turquoise, White, Coral</p>
', 0.7, 30, 30, 30, NULL, N'LowStock', 1, 0, N'item', N'AUD', 10, 15, 90, 90, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T11:43:12.130' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Turquoise, White, ', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2183, 1026, N'HL06', N'SBB1022', N'Colorful', N'SBB1022.Colorful', N'Caro Basket', N'<p>Dimension (inch): Height 8.66&#39; x Width 13.78&#39; x Length 13.78&#39;<br />
Dimension (cm): Height 22cm x Width 35cm x Length 35cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, White, Silver, Coral</p>
', 0.6, 22, 35, 35, NULL, N'LowStock', 1, 0, N'item', N'AUD', 6.5, 9.75, 58.5, 58.5, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T11:46:51.917' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', 1025, NULL, N'Seagrass, Plastic', N'Seagrass, White, Silver, Coral', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2184, 1024, N'HL15', N'KU1030', N'BlackWhite', N'KU1030.BlackWhite', N'Hatti Bowl', N'<p>Dimension (inch): Height 2.36&#39; x Width 7.87&#39; x Length 7.87&#39;<br />
Dimension (cm): Height 6cm x Width 20cm x Length 20cm<br />
Materials: Rattan<br />
Colors: Rattan</p>
', 0.3, 10, 23, 23, NULL, N'LowStock', 1, 0, N'item', N'AUD', 3.5, 5.25, 31.5, 31.5, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T11:53:26.987' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, White, Black', NULL)
INSERT [dbo].[Products] ([Id], [CategoryId], [OriginalProductCode], [Namekey], [ColorCode], [ProductCode], [Name], [Description], [Weight], [Height], [Width], [Length], [MoreInfo], [InStock], [StockLevel], [IsLive], [Unit], [UnitOfPrice], [ImportPriceUSD], [ImportPrice], [OriginalPrice], [DiscountPrice], [NumberOfRating], [NumberOfLiked], [NumberOfReviewed], [AverageRating], [PrimaryImage], [SubImage1], [SubImage2], [SubImage3], [SubImage4], [SubImage5], [ExchangeRate], [OriginalRate], [DiscountRate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [SubCategoryId1], [SubCategoryId2], [Materials], [Colors], [ShareLink]) VALUES (2185, 1024, N'HL16', N'KU1031', N'Colorful', N'KU1031.Colorful', N'Kila Bowl', N'<p>Dimension (inch): Height 5.91&#39; x Width 12.60&#39; x Length 12.60&#39;<br />
Dimension (cm): Height 15cm x Width 32cm x Length 32cm<br />
Materials: Seagrass, Plastic<br />
Colors: Seagrass, Black, White, Red, Tear</p>
', 0.5, 15, 32, 32, NULL, N'LowStock', 1, 0, N'item', N'AUD', 4.5, 6.75, 40.5, 40.5, 0, 0, 0, 0, N'\images\app\default_product.jpg', NULL, NULL, NULL, NULL, NULL, 1.5, 6, 5, CAST(N'2018-12-21T12:02:04.163' AS DateTime), N'admin', CAST(N'2019-01-17T06:34:27.593' AS DateTime), N'admin', NULL, NULL, N'Seagrass, Plastic', N'Seagrass, Black, White, Red, Tear', NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Promotions] ON 

INSERT [dbo].[Promotions] ([Id], [PromotionType], [NameOfPromotion], [PromotionCode], [Description], [Percentage], [Dollars], [AutoApply], [StartDate], [EndDate], [LimitTime], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (9, 1, N'Grand Opening For 50%', N'GO122018', NULL, 50, NULL, 0, CAST(N'2018-12-07T00:00:00.000' AS DateTime), CAST(N'2018-09-10T23:59:00.000' AS DateTime), 4, 0, N'admin', CAST(N'2018-12-05T17:21:41.730' AS DateTime), N'admin', CAST(N'2018-12-10T12:30:57.847' AS DateTime))
INSERT [dbo].[Promotions] ([Id], [PromotionType], [NameOfPromotion], [PromotionCode], [Description], [Percentage], [Dollars], [AutoApply], [StartDate], [EndDate], [LimitTime], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (10, 3, N'Free Shipping', N'FSLOVE2019', N'Free shipping for some members', 100, NULL, 0, NULL, NULL, 18, 1, N'admin', CAST(N'2018-12-05T22:53:29.457' AS DateTime), N'admin', CAST(N'2019-01-22T06:06:25.000' AS DateTime))
INSERT [dbo].[Promotions] ([Id], [PromotionType], [NameOfPromotion], [PromotionCode], [Description], [Percentage], [Dollars], [AutoApply], [StartDate], [EndDate], [LimitTime], [IsLive], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES (11, 1, N'15% For Friends', N'15FORFRIEND', N'15% For Friends', 15, NULL, 0, CAST(N'2018-12-07T00:00:00.000' AS DateTime), CAST(N'2018-12-31T23:59:00.000' AS DateTime), NULL, 1, N'admin', CAST(N'2018-12-07T01:36:31.963' AS DateTime), N'admin', CAST(N'2018-12-07T06:47:30.887' AS DateTime))
SET IDENTITY_INSERT [dbo].[Promotions] OFF
SET IDENTITY_INSERT [dbo].[ShoppingCart] ON 

INSERT [dbo].[ShoppingCart] ([Id], [TotalAmountExGst], [TotalAmountIncGst], [Gst], [ShippingFee], [Discount], [TotalFinalPrice], [EstimateDelivery], [PromotionId], [ShippingPromotionId], [GSTPercent], [CreditCardSurcharge], [PayPalSurcharge], [FreeShippingAusPostFrom], [FreeShippingHomeMailFrom], [HomeMailDefaultPrice1], [HomeMailDefaultPrice2], [HomeMailDefaultPrice3], [HomeMailAvailableFrom], [WarehousePostcode], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2184, 49, 49, 0, 0, 24.5, 24.5, N'Delivered in 2 business days', 9, 10, 0, 1.5, 1.5, 199.95, 159.95, 7.55, 9.55, 11.55, 99.95, 3023, CAST(N'2018-12-06T23:41:32.540' AS DateTime), N'System', CAST(N'2018-12-06T23:41:52.147' AS DateTime), N'System')
INSERT [dbo].[ShoppingCart] ([Id], [TotalAmountExGst], [TotalAmountIncGst], [Gst], [ShippingFee], [Discount], [TotalFinalPrice], [EstimateDelivery], [PromotionId], [ShippingPromotionId], [GSTPercent], [CreditCardSurcharge], [PayPalSurcharge], [FreeShippingAusPostFrom], [FreeShippingHomeMailFrom], [HomeMailDefaultPrice1], [HomeMailDefaultPrice2], [HomeMailDefaultPrice3], [HomeMailAvailableFrom], [WarehousePostcode], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2199, 25, 25, 0, 0, 3.75, 21.25, N'Delivered in 2 business days', 11, 10, 0, 1.5, 1.5, 199.95, 159.95, 7.55, 9.55, 11.55, 99.95, 3023, CAST(N'2018-12-10T11:16:08.773' AS DateTime), N'System', CAST(N'2018-12-10T11:18:38.297' AS DateTime), N'System')
INSERT [dbo].[ShoppingCart] ([Id], [TotalAmountExGst], [TotalAmountIncGst], [Gst], [ShippingFee], [Discount], [TotalFinalPrice], [EstimateDelivery], [PromotionId], [ShippingPromotionId], [GSTPercent], [CreditCardSurcharge], [PayPalSurcharge], [FreeShippingAusPostFrom], [FreeShippingHomeMailFrom], [HomeMailDefaultPrice1], [HomeMailDefaultPrice2], [HomeMailDefaultPrice3], [HomeMailAvailableFrom], [WarehousePostcode], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2207, 433.43, 433.43, 0, 0, 0, 433.43, N'Delivered in 2 business days', NULL, 10, 0, 1.5, 1.5, 199.95, 159.95, 7.55, 9.55, 11.55, 99.95, 3023, CAST(N'2018-12-15T12:41:33.203' AS DateTime), N'admin', CAST(N'2019-01-22T06:06:32.763' AS DateTime), N'admin')
INSERT [dbo].[ShoppingCart] ([Id], [TotalAmountExGst], [TotalAmountIncGst], [Gst], [ShippingFee], [Discount], [TotalFinalPrice], [EstimateDelivery], [PromotionId], [ShippingPromotionId], [GSTPercent], [CreditCardSurcharge], [PayPalSurcharge], [FreeShippingAusPostFrom], [FreeShippingHomeMailFrom], [HomeMailDefaultPrice1], [HomeMailDefaultPrice2], [HomeMailDefaultPrice3], [HomeMailAvailableFrom], [WarehousePostcode], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2208, 54, 54, 0, 0, 8.1, 45.9, N'Delivered in 2 business days', 11, 10, 0, 1.5, 1.5, 199.95, 159.95, 7.55, 9.55, 11.55, 99.95, 3023, CAST(N'2018-12-16T07:27:29.603' AS DateTime), N'System', CAST(N'2018-12-16T07:39:18.910' AS DateTime), N'System')
INSERT [dbo].[ShoppingCart] ([Id], [TotalAmountExGst], [TotalAmountIncGst], [Gst], [ShippingFee], [Discount], [TotalFinalPrice], [EstimateDelivery], [PromotionId], [ShippingPromotionId], [GSTPercent], [CreditCardSurcharge], [PayPalSurcharge], [FreeShippingAusPostFrom], [FreeShippingHomeMailFrom], [HomeMailDefaultPrice1], [HomeMailDefaultPrice2], [HomeMailDefaultPrice3], [HomeMailAvailableFrom], [WarehousePostcode], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2212, 33.75, 33.75, 0, 15.25, 0, 49, N'Delivered in 2 business days', NULL, NULL, 0, 1.5, 1.5, 99.99, 79.99, 7.55, 9.55, 11.55, 99.95, 3023, CAST(N'2018-12-21T03:22:40.800' AS DateTime), N'System', CAST(N'2018-12-21T03:22:41.707' AS DateTime), N'System')
SET IDENTITY_INSERT [dbo].[ShoppingCart] OFF
SET IDENTITY_INSERT [dbo].[UserProfiles] ON 

INSERT [dbo].[UserProfiles] ([UserId], [UserName], [FirstName], [LastName], [Gender], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [Avatar], [IsLive], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (17, N'user01', N'Merav', N'Kazt', 0, N'Future Business System', N'284 Road Bay', N'Cheltenham', N'3192', N'Victoria', N'Australia', N'user01@gmail.com', N'0420000000', N'\images\app\female_avatar_1.png', 1, CAST(N'2018-08-13T23:20:27.913' AS DateTime), N'admin', CAST(N'2018-09-14T10:34:16.457' AS DateTime), N'admin')
INSERT [dbo].[UserProfiles] ([UserId], [UserName], [FirstName], [LastName], [Gender], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [Avatar], [IsLive], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (18, N'HaVu-13082018', N'Ha', N'Vu', 0, N'Clarke Rocca', N'2 Doyle Street', N'Sunshine North', N'3020', N'Victoria', N'Australia', N'hannahvu11@gmail.com', N'0425889939', N'\images\app\female_avatar.png', 1, CAST(N'2018-08-13T23:21:14.573' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[UserProfiles] ([UserId], [UserName], [FirstName], [LastName], [Gender], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [Avatar], [IsLive], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (19, N'user02', N'Peter', N'Rowsell', 1, N'Optus', N'20 Ambon Avenue', N'Deer Park', N'3023', N'Victoria', N'Australia', N'user02@gmail.com', N'0420833757', N'\images\app\male_avatar_1.png', 1, CAST(N'2018-08-13T23:25:00.277' AS DateTime), N'admin', CAST(N'2018-09-14T10:38:04.957' AS DateTime), N'admin')
INSERT [dbo].[UserProfiles] ([UserId], [UserName], [FirstName], [LastName], [Gender], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [Avatar], [IsLive], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (20, N'admin', N'Thanh', N'Vo', 1, N'Cognology', N'Level 35, 600 Bourke Street', N'Melbourne', N'3000', N'Victoria', N'Australia', N'thanhvo.melbourne@gmail.com', N'0420833757', N'\images\app\male_avatar_1.png', 1, CAST(N'2018-09-14T10:36:57.117' AS DateTime), N'admin', CAST(N'2018-12-07T10:11:09.833' AS DateTime), N'admin')
INSERT [dbo].[UserProfiles] ([UserId], [UserName], [FirstName], [LastName], [Gender], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [Avatar], [IsLive], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (21, N'user03', N'David', N'Ying', 1, N'Future Business System', N'284 Bay Road', N'Cheltenham', N'3192', N'Victoria', N'Australia', N'user03@gmail.com', N'0420000000', N'\images\app\male_avatar_1.png', 1, CAST(N'2018-09-14T10:42:02.940' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[UserProfiles] ([UserId], [UserName], [FirstName], [LastName], [Gender], [CompanyName], [Address], [Suburb], [Postcode], [State], [Country], [Email], [Phone], [Avatar], [IsLive], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (22, N'user04', N'Natalie', N'Ong', 0, N'AKQA', N'380 St Kilda Road', N'St Kilda', N'3001', N'Victoria', N'Australia', N'user04@gmail.com', N'0420000000', N'\images\app\female_avatar_1.png', 1, CAST(N'2018-09-14T10:45:28.010' AS DateTime), N'admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserProfiles] OFF
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (17, CAST(N'2018-08-13T13:20:28.010' AS DateTime), NULL, 1, NULL, 0, N'AE8eUSRXJCAqZAtBkaFzSatoPdQPb4kehwec3ICdMLRaj4wbToC165eq8QpmEJ4GUQ==', CAST(N'2018-08-13T13:20:28.010' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (18, CAST(N'2018-08-13T13:21:14.597' AS DateTime), NULL, 1, CAST(N'2018-08-13T13:41:01.287' AS DateTime), 0, N'AGC0DaTly0Ndj4KC8YeK0EJpa8PXqbR9boGsWaASuFGIMlIJVhQHPrw25c9H/wcdCg==', CAST(N'2018-12-20T05:31:59.653' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (19, CAST(N'2018-08-13T13:25:00.440' AS DateTime), NULL, 1, CAST(N'2018-09-14T01:12:31.683' AS DateTime), 0, N'ABpE9lnmsMs1DYp1R/coUPVpcGGv8cmVg3hejWOPzE45/mVb97HL2WP70RrLVnYIlg==', CAST(N'2018-08-25T12:10:22.990' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (20, CAST(N'2018-09-14T00:36:57.183' AS DateTime), NULL, 1, CAST(N'2019-01-05T10:12:22.677' AS DateTime), 0, N'AA7IXePKdaBKdf5E+qLOW7DithMpyeo8/vcXpurW6qOivs2SDIGV1GFgNfcPfefY+g==', CAST(N'2018-12-06T23:11:10.317' AS DateTime), N'', N'7YrsvqWSNoG8bIEq4mU16Q2', CAST(N'2018-12-16T05:30:00.820' AS DateTime))
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (21, CAST(N'2018-09-14T00:42:02.997' AS DateTime), NULL, 1, NULL, 0, N'AClRk0HR53k6BXlnIrGGFYfFwkmrY9hc/ZVDKEhVIuQMVlpd2A7ufw2TaAicI2wkQA==', CAST(N'2018-09-14T00:42:02.997' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (22, CAST(N'2018-09-14T00:45:28.050' AS DateTime), NULL, 1, NULL, 0, N'AGBRcxd/uFrBt0jH0MThBukKZwtX8ZZnV3psh2+0QjoJ9hlrtaTM400oXoj+qYcCZw==', CAST(N'2018-09-14T00:45:28.050' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'Customer')
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (18, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (20, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (17, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (19, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (21, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (22, 2)
/****** Object:  Index [IX_FK_webpages_UsersInRoles_webpages_Roles]    Script Date: 23/01/2019 12:54:38 ******/
CREATE NONCLUSTERED INDEX [IX_FK_webpages_UsersInRoles_webpages_Roles] ON [dbo].[webpages_UsersInRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationSettings] ADD  CONSTRAINT [DF_ApplicationSettings_GSTPercent]  DEFAULT ((0)) FOR [GSTPercent]
GO
ALTER TABLE [dbo].[ApplicationSettings] ADD  CONSTRAINT [DF_ApplicationSettings_CreditCardSurcharge]  DEFAULT ((0)) FOR [CreditCardSurcharge]
GO
ALTER TABLE [dbo].[ApplicationSettings] ADD  CONSTRAINT [DF_ApplicationSettings_PaypalSurcharge]  DEFAULT ((0)) FOR [PaypalSurcharge]
GO
ALTER TABLE [dbo].[ApplicationSettings] ADD  CONSTRAINT [DF_ApplicationSettings_HomeMailDefaultPrice]  DEFAULT ((0)) FOR [HomeMailDefaultPrice1]
GO
ALTER TABLE [dbo].[ApplicationSettings] ADD  CONSTRAINT [DF_ApplicationSettings_HomeMailDefaultPrice2]  DEFAULT ((0)) FOR [HomeMailDefaultPrice2]
GO
ALTER TABLE [dbo].[ApplicationSettings] ADD  CONSTRAINT [DF_ApplicationSettings_WarehousePostcode]  DEFAULT ((0)) FOR [WarehousePostcode]
GO
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_IsRead]  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[EVoucher] ADD  CONSTRAINT [DF_EVoucher_IsGift]  DEFAULT ((0)) FOR [IsGift]
GO
ALTER TABLE [dbo].[EVoucher] ADD  CONSTRAINT [DF_EVoucher_IsLive]  DEFAULT ((1)) FOR [IsLive]
GO
ALTER TABLE [dbo].[Payments] ADD  CONSTRAINT [DF_Payments_IsFull]  DEFAULT ((1)) FOR [IsFull]
GO
ALTER TABLE [dbo].[PreOrder] ADD  CONSTRAINT [DF_PreOrder_UserId]  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[PreOrder] ADD  CONSTRAINT [DF_PreOrder_CustomerEscalate]  DEFAULT ((0)) FOR [CustomerEscalate]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Weight]  DEFAULT ((0)) FOR [Weight]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Height]  DEFAULT ((0)) FOR [Height]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Width]  DEFAULT ((0)) FOR [Width]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Length]  DEFAULT ((0)) FOR [Length]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_StockLevel]  DEFAULT ((0)) FOR [StockLevel]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_NumberOfReviewed]  DEFAULT ((0)) FOR [NumberOfReviewed]
GO
ALTER TABLE [dbo].[Promotions] ADD  CONSTRAINT [DF_Promotions_AutoApply]  DEFAULT ((0)) FOR [AutoApply]
GO
ALTER TABLE [dbo].[Promotions] ADD  CONSTRAINT [DF_Promotions_IsLive]  DEFAULT ((0)) FOR [IsLive]
GO
ALTER TABLE [dbo].[ReviewedProduct] ADD  CONSTRAINT [DF_ReviewedProduct_Rating]  DEFAULT ((0)) FOR [Rating]
GO
ALTER TABLE [dbo].[ShoppingCart] ADD  CONSTRAINT [DF_ShoppingCart_ShippingFee]  DEFAULT ((0)) FOR [ShippingFee]
GO
ALTER TABLE [dbo].[ShoppingCart] ADD  CONSTRAINT [DF_ShoppingCart_GSTPercent]  DEFAULT ((0)) FOR [GSTPercent]
GO
ALTER TABLE [dbo].[ShoppingCart] ADD  CONSTRAINT [DF_ShoppingCart_CreditCardSurcharge]  DEFAULT ((0)) FOR [CreditCardSurcharge]
GO
ALTER TABLE [dbo].[ShoppingCart] ADD  CONSTRAINT [DF_ShoppingCart_PayPalSurcharge]  DEFAULT ((0)) FOR [PayPalSurcharge]
GO
ALTER TABLE [dbo].[UserProfiles] ADD  CONSTRAINT [DF_UserProfiles_Gender]  DEFAULT ((0)) FOR [Gender]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_Products1] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_Products1]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_ShoppingCart] FOREIGN KEY([CartId])
REFERENCES [dbo].[ShoppingCart] ([Id])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_ShoppingCart]
GO
ALTER TABLE [dbo].[LikedProduct]  WITH CHECK ADD  CONSTRAINT [FK_LikedProduct_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[LikedProduct] CHECK CONSTRAINT [FK_LikedProduct_Products]
GO
ALTER TABLE [dbo].[LikedProduct]  WITH CHECK ADD  CONSTRAINT [FK_LikedProduct_UserProfiles] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfiles] ([UserId])
GO
ALTER TABLE [dbo].[LikedProduct] CHECK CONSTRAINT [FK_LikedProduct_UserProfiles]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_ShoppingCart] FOREIGN KEY([CartId])
REFERENCES [dbo].[ShoppingCart] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_ShoppingCart]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Orders]
GO
ALTER TABLE [dbo].[PreOrder]  WITH CHECK ADD  CONSTRAINT [FK_PreOrder_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[PreOrder] CHECK CONSTRAINT [FK_PreOrder_Products]
GO
ALTER TABLE [dbo].[PreOrder]  WITH CHECK ADD  CONSTRAINT [FK_PreOrder_UserProfiles] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfiles] ([UserId])
GO
ALTER TABLE [dbo].[PreOrder] CHECK CONSTRAINT [FK_PreOrder_UserProfiles]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories1] FOREIGN KEY([SubCategoryId1])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories1]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories2] FOREIGN KEY([SubCategoryId2])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories2]
GO
ALTER TABLE [dbo].[ReviewedProduct]  WITH CHECK ADD  CONSTRAINT [FK_ReviewedProduct_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ReviewedProduct] CHECK CONSTRAINT [FK_ReviewedProduct_Products]
GO
ALTER TABLE [dbo].[ReviewedProduct]  WITH CHECK ADD  CONSTRAINT [FK_ReviewedProduct_UserProfiles] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfiles] ([UserId])
GO
ALTER TABLE [dbo].[ReviewedProduct] CHECK CONSTRAINT [FK_ReviewedProduct_UserProfiles]
GO
ALTER TABLE [dbo].[ShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCart_Promotions] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotions] ([Id])
GO
ALTER TABLE [dbo].[ShoppingCart] CHECK CONSTRAINT [FK_ShoppingCart_Promotions]
GO
ALTER TABLE [dbo].[ShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCart_Promotions1] FOREIGN KEY([ShippingPromotionId])
REFERENCES [dbo].[Promotions] ([Id])
GO
ALTER TABLE [dbo].[ShoppingCart] CHECK CONSTRAINT [FK_ShoppingCart_Promotions1]
GO
ALTER TABLE [dbo].[webpages_Membership]  WITH CHECK ADD  CONSTRAINT [FK_webpages_Membership_UserProfiles] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfiles] ([UserId])
GO
ALTER TABLE [dbo].[webpages_Membership] CHECK CONSTRAINT [FK_webpages_Membership_UserProfiles]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_webpages_UsersInRoles_webpages_Membership] FOREIGN KEY([UserId])
REFERENCES [dbo].[webpages_Membership] ([UserId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [FK_webpages_UsersInRoles_webpages_Membership]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_webpages_UsersInRoles_webpages_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [FK_webpages_UsersInRoles_webpages_Roles]
GO
ALTER TABLE [dbo].[Wishlist]  WITH CHECK ADD  CONSTRAINT [FK_Wishlist_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Wishlist] CHECK CONSTRAINT [FK_Wishlist_Products]
GO
ALTER TABLE [dbo].[Wishlist]  WITH CHECK ADD  CONSTRAINT [FK_Wishlist_UserProfiles] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfiles] ([UserId])
GO
ALTER TABLE [dbo].[Wishlist] CHECK CONSTRAINT [FK_Wishlist_UserProfiles]
GO
USE [master]
GO
ALTER DATABASE [househomelove] SET  READ_WRITE 
GO
