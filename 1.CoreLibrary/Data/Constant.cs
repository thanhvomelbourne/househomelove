using System;
using System.ComponentModel;
using System.Web;

namespace CoreLibrary.Data
{
    public static class Constant
    {
        public enum PromotionType
        {
            [Description("Discount Total On Percentage")]
            DiscountTotalOnPercentage = 1,

            [Description("Discount Total On Dollars")]
            DiscountTotalOnDollars = 2,

            [Description("Discount Shipping")]
            DiscountShipping = 3
        }

        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        public static string GetDescriptionTextForCurrentEnum(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        public static class FieldNames
        {
            public const string CreatedBy = "CreatedBy";
            public const string CreatedAt = "CreatedAt";
            public const string UpdatedBy = "UpdatedBy";
            public const string UpdatedAt = "UpdatedAt";
        }

        public static class SizeTypes
        {
            public const int Small = 1;
            public const int Medium = 2;
            public const int Large = 3;
        }

        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Customer = "Customer";
        }

        public static class CommonValue
        {
            public const int PageSize20 = 20;
            public const int PageSize40 = 40;
            public const string Male = "Male";
            public const string Female = "Female";
            public const int EVoucherProductId = 2107;
            public const string SortByNewest = "Newest";
            public const string SortByPriceLowToHigh = "PriceLowToHigh";
            public const string SortByPriceHighToLow = "PriceHighToLow";
            public const string InStock = "InStock";
            public const string LowStock = "LowStock";
            public const string OutOfStock = "OutOfStock";
            public const string TechnicalEmail = "thanhvo.melbourne@gmail.com";
            public const string EncryptKey = "thanhvohousehomelove";
            public const string IsLive = "IsLive";
            public const string NotLive = "NotLive";
            public const string All = "All";
            public const int DefaultEstimatedShipping = 7;
            public static TimeZoneInfo AustralianEasternTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
        }

        public static class EntityStatus
        {
            public const bool Active = true;
            public const bool Inactive = false;
        }

        public static class DefaultImagePath
        {
            public const string DefaultFemaleAvatar = @"\images\app\female_avatar.png";
            public const string DefaultFemaleAvatar1 = @"\images\app\female_avatar_1.png";
            public const string DefaultFemaleAvatar2 = @"\images\app\female_avatar_2.png";

            public const string DefaultMaleAvatar = @"\images\app\male_avatar.png";
            public const string DefaultMaleAvatar1 = @"\images\app\male_avatar_1.png";
            public const string DefaultMaleAvatar2 = @"\images\app\male_avatar_2.png";

            public const string DefaultAdminAvatar = @"\images\app\admin_avatar.png";

            public const string DefaultGenericAvatar = @"\images\app\avatar_generic_small.png";

            public const string DefaultEVoucher = @"\images\app\voucher.svg";
            public const string DefaultBackground = @"\images\app\default_background.jpg";
            public const string DefaultProduct = @"\images\app\default_product.jpg";
            public const string DefaultBanner = @"\images\app\default_banner.jpg";
        }

        public static class Path
        {
            public const string Domain = "https://househomelove.com.au/";
            public const string ProductImagePath = @"\images\product_images\";
            public const string AppImagePath = @"\images\banner_icon_images\";
            public const string EventImagePath = @"\images\events\";
            public const string RecoveryPassword = Domain + "resetpassword";
            public const string ShareLink = "https://api.branch.io/v1/url";
            public static bool IsLive = HttpContext.Current.Request.Url.AbsoluteUri.Contains("househomelove.com.au");
            public const string QRCodePath = @"\images\qr_code\";
            public const string QRCodeForProduct = "https://househomelove.com.au/shop/product?productId=";
        }

        public static class ConfirmedStatus
        {
            public const string Succeed = "SUCCEED";
            public const string SucceedAndConfirmed = "SUCCEED & CONFIRMED";
            public const string Failed = "FAILED";
            public const string Processing = "PROCESSING";
            public const string Paidapart = "PAID A PART";
        }

        public static class PaymentMethod
        {
            public const string CreditCard = "CreditCard";
            public const string AfterPay = "AfterPay";
            public const string PayPal = "PayPal";
            public const string EVoucher = "EVoucher";
            public const string ZipPay = "ZipPay";
        }

        public enum ShippingMethodType
        {
            [Description("Australia Post")]
            AustraliaPost = 1,

            [Description("Home Mail")]
            HomeMail = 2,

            [Description("Click and Collect")]
            ClickAndCollect = 3
        }

        public static class ShippingMethod
        {
            public const string AustraliaPost = "AUS_POST";
            public const string HomeMail = "HOME_MAIL";
            public const string ClickAndCollect = "CLICK_AND_COLLECT";
        }

        public static class PreOrderStatus
        {
            public const string ReceivedPreOrder = "Received Pre-Order";
            public const string ApprovedPreOrder = "Approved Pre-Order";
            public const string DeclinedPreOrder = "Declined Pre-Order";
            public const string PreOrderIsReady = "Pre-Order Is Ready";
            public const string ClosePreOrder = "Close Pre-Order";
        }

        public static class AustraliaPost
        {
            public const string DefaultShippingTo = "3000";
            public const string Token = "111e64ba-66aa-4b25-948c-3bdfd06d56ac";
            public const string CurrentWarehousePostcode = "3023";
            public const string CallErrorMessage = "Cannot call to AusPost API";
            public const double DefaultShippingFee = 15;
            public const string DefaultEstimateDelivery = "Delivered in 5-7 business days.";
        }

        public static class EmailType
        {
            public const string ConfirmationEmail = "ConfirmationEmail";
            public const string RecoveryPasswordEmail = "RecoveryPasswordEmail";
            public const string RegisterAccountEmail = "RegisterAccountEmail";
            public const string SystemErrorEmail = "SystemErrorEmail";
            public const string SystemTestEmail = "SystemTestEmail";
            public const string NewsletterEmail = "NewsletterEmail";

            public const string ConfirmationEmailTemplate =
                "https://househomelove.com.au/EmailTemplate/ConfirmationOrder.html";

            public const string RecoveryPasswordEmailTemplate =
                "https://househomelove.com.au/EmailTemplate/RecoveryPassword.html";

            public const string RegistrationEmailTemplate =
                "https://househomelove.com.au/EmailTemplate/RegisterAccount.html";

            public const string SystemErrorEmailTemplate =
                "https://househomelove.com.au/EmailTemplate/SystemError.html";

            public const string SystemTestEmailTemplate =
               "https://househomelove.com.au/EmailTemplate/SystemTest.html";

            public const string NewsletterEmailTemplate =
               "https://househomelove.com.au/EmailTemplate/Newsletter.html";
        }

        public static class PromotionTypeId
        {
            public const int DiscountTotalOnPercentage = 1;
            public const int DiscountTotalOnDollars = 2;
            public const int DiscountShipping = 3;
        }
        public enum GroupType
        {
            [Description("Group by Size")]
            GroupBySize = 1,

            [Description("Group by Color")]
            GroupByColor = 2
        }

        public static class GroupTypeId
        {
            public const int GroupBySize = 1;
            public const int GroupByColor = 2;
        }

        public enum HomeMailServicePostcode
        {
            [Description("Melbourne")]
            Melbourne_1 = 3000,

            [Description("East Nelbourne")]
            East_Melbourne = 3002,

            [Description("West Melbourne")]
            West_Melbourne = 3003,

            [Description("Melbourne")]
            Melbourne_2 = 3004,

            [Description("Docklands")]
            Docklands_1 = 3005,

            [Description("Southbank")]
            Southbanks = 3006,

            [Description("Docklands")]
            Docklands_2 = 3008,

            [Description("Parkville")]
            Parkville = 3010,

            [Description("Footscray, Seddon")]
            Footscray = 3011,

            [Description("Maidstone, West Footscray, Kingsville, Brooklyn, Tottenham")]
            Maidstone = 3012,

            [Description("Yarraville")]
            Yarraville = 3013,

            [Description("Spotswood, South Kingsville, Newport")]
            South_Kingsville = 3015,

            [Description("Williamstown, Williamstown North")]
            Williamstown = 3016,

            [Description("Braybrook")]
            Braybrook = 3019,

            [Description("Sunshine, Sunshine West, Sunshine North, Albion")]
            Sunshine = 3020,

            [Description("St Albans, Kealba, Albanvale, Kings Park")]
            St_Albans = 3021,

            [Description("Ardeer")]
            Ardeer = 3022,

            [Description("Cairnlea, Deer Park, Ravenhall, Caroline Springs, Burnside Heights, Burnside")]
            DeerPark = 3023,

            [Description("Altona North")]
            Altona_North = 3025,

            [Description("Laverton North")]
            Laverton_North = 3026,

            [Description("Williams Landing")]
            Williams_Landing = 3027,

            [Description("Flemington, Kensington")]
            Flemington = 3031,

            [Description("Maribyrnong, Ascot Vale, Travancore")]
            Maribyrnong = 3032,

            [Description("Keilor East")]
            Keilor_East = 3033,

            [Description("Avondale Heights")]
            Avondale_Heights = 3034,

            [Description("Keilor, Keilor North")]
            Keilor = 3036,

            [Description("Taylors Hill, Sydenham, Hillside, Delahey, Calder Park")]
            Sydenham = 3037,

            [Description("Taylors Lakes, Keilor Downs, Keilor Lodge")]
            Taylors_Lakes = 3038,

            [Description("Moonee Ponds")]
            Moonee_Ponds = 3039,

            [Description("Aberfeldie, Essendon West, Essendon")]
            Essendon = 3040,

            [Description("Strathmore, Essendon North, Strathmore Heights, Essendon Fields")]
            Strathmore = 3041,

            [Description("Airport West, Niddrie, Keilor Park")]
            Airport_West = 3042,

            [Description("Gladstone Park, Tullamarine, Gowanbrae")]
            Tullamarine = 3043,

            [Description("Pascoe Vale, Pascoe Vale South")]
            Pascoe_Vale = 3044,

            [Description("Melbourne Airport")]
            Melbourne_Airport = 3045,

            [Description("Glenroy, Oak Park, Hadfield")]
            Glenroy = 3046,

            [Description("Melbourne Hospital - Parkville")]
            Melbourne_Hospital = 3050,

            [Description("North Melbourne")]
            North_Melbourne = 3051,

            [Description("Melbourne Zoo - Parkville")]
            Melbourne_Zoo = 3052,

            [Description("Carlton")]
            Carlton = 3053,

            [Description("Carlton North, Princes Hill")]
            Carlton_North = 3054,

            [Description("Brunswick West")]
            Brunswick_West = 3055,

            [Description("Brunswick")]
            Brunswick = 3056,

            [Description("Brunswick East")]
            Brunswick_East = 3057,

            [Description("Coburg, Coburg North")]
            Coburg = 3058,

            [Description("Fitzroy")]
            Fitzroy = 3065,

            [Description("Collingwood")]
            Collingwood = 3066,

            [Description("Abbotsford")]
            Abbotsford = 3067,

            [Description("Clifton Hill, Fitzroy North")]
            Clifton_Hill = 3068,

            [Description("Northcote")]
            Northcote = 3070,

            [Description("Thornbury")]
            Thornbury = 3071,

            [Description("Preston")]
            Preston = 3072,

            [Description("Reservoir")]
            Reservoir = 3073,

            [Description("Fairfield")]
            Fairfield = 3078,

            [Description("Ivanhoe, Ivanhoe East")]
            Ivanhoe = 3079,

            [Description("Heidelberg West, Bellfield, Heidelberg Heights")]
            Bellfield = 3081,

            [Description("Kew")]
            Kew = 3101,

            [Description("Kew East")]
            Kew_East = 3102,

            [Description("Balwyn, Deepdene")]
            Balwyn = 3103,

            [Description("Burnley")]
            Burnley = 3121,

            [Description("Hawthorn")]
            Hawthorn = 3122,

            [Description("Hawthorn East")]
            Hawthorn_East = 3123,

            [Description("Camberwell")]
            Camberwell = 3124,

            [Description("Canterbury")]
            Canterbury = 3126,

            [Description("South Yarra")]
            South_Yarra = 3141,

            [Description("Hawksburn")]
            Hawksburn = 3142,

            [Description("Armadale")]
            Armadale = 3143,

            [Description("Malvern")]
            Malvern = 3144,

            [Description("Malvern East")]
            Malvern_East = 3145,

            [Description("Caulfield North")]
            Caulfield_North = 3161,

            [Description("Caulfield, Caulfield South")]
            Caulfield_South = 3162,

            [Description("Prahran, Windsor")]
            Prahran = 3181,

            [Description("St Kilda")]
            St_Kilda = 3182,

            [Description("St Kilda East, Balaclava")]
            Balaclava = 3183,

            [Description("Elwood")]
            Elwood = 3184,

            [Description("Elsternwick")]
            Elsternwick = 3185,

            [Description("Brighton")]
            Brighton = 3186,

            [Description("Brighton East")]
            Brighton_East = 3187,
        }
    }
}