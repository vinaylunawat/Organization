namespace Framework.Service.Criteria
{
    using Framework.Service.Paging;

    public class FilterModel
    {
        public SearchingStrategy SearchingStrategy { get; set; }

        public OrderingStrategy OrderingInfo { get; set; }

        public PagingStrategy PagingInfo { get; set; }
    }
}
