using System.ComponentModel;

namespace OnlineStore.Contracts.Enums
{
    public enum ProductAttributeTypeEnum
    {
        [Description("Используется для поиска")]
        ForSearch = 1,

        [Description("Не используется для поиска")]
        NotForSearch = 2
    }
}
