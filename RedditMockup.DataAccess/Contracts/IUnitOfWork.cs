using RedditMockup.DataAccess.Repositories;

namespace RedditMockup.DataAccess.Contracts;

public interface IUnitOfWork
{
    #region [Properties]

    AnswerRepository? AnswerRepository { get; }

    PersonRepository? PersonRepository { get; }

    ProfileRepository? ProfileRepository { get; }

    QuestionRepository? QuestionRepository { get; }

    RoleRepository? RoleRepository { get; }

    UserRepository? UserRepository { get; }

    UserRoleRepository? UserRoleRepository { get; }

    QuestionVoteRepository? QuestionVoteRepository { get; }

    AnswerVoteRepository? AnswerVoteRepository { get; }

    BookmarkRepository? BookmarkRepository { get; }

    #endregion

    #region [Methods]

    int Commit();

    Task<int> CommitAsync(CancellationToken cancellationToken);

    #endregion
}