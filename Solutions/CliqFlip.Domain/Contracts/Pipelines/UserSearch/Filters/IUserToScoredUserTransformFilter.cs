namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface IUserToScoredUserTransformFilter
    {
        void Filter(UserSearchPipelineResult userSearchPipelineResult);
    }
}