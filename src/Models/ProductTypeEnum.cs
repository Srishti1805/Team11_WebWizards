namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// enum for Product/Game Category
    /// </summary>
	public enum ProductTypeEnum
	{
		Undefined = 0,
        HomeAppliance = 1,
        GardeningEquipments = 2,
        SnowEquipments = 3,
        CampingEquipments = 4,
	}
    /// <summary>
    /// Representing class enum for product/game category
    /// Grouping products/games together by category
    /// </summary>
    public static class ProductTypeEnumExtensions
    {
        /// <summary>
        /// enum value is displayed as a string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DisplayName(this ProductTypeEnum data)
        {
            return data switch
            {
                ProductTypeEnum.HomeAppliance => "Home Appliances",
                ProductTypeEnum.GardeningEquipments => "Gardening Equipments",
                ProductTypeEnum.SnowEquipments => "Snow Equipments",
                ProductTypeEnum.CampingEquipments => "Camping Equipments",
                ProductTypeEnum.Undefined => "Undefined",
                _ => ""
            };
        }
    }
}