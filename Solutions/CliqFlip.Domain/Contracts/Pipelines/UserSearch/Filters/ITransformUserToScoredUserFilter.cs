namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface ITransformUserToScoredUserFilter
    {
        void Filter(UserSearchPipelineResult userSearchPipelineResult);
    }
}