using RedditMockup.DataAccess.Context;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using Sieve.Services;

namespace RedditMockup.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private AnswerRepository? _answerRepository;

    private PersonRepository? _personRepository;

    private ProfileRepository? _profileRepository;

    private QuestionRepository? _questionRepository;

    private RoleRepository? _roleRepository;

    private UserRepository? _userRepository;

    private UserRoleRepository? _userRoleRepository;

    private AnswerVoteRepository? _answerVoteRepository;

    private QuestionVoteRepository? _questionVoteRepository;

    private BookmarkRepository? _bookmarkRepository;

    private readonly RedditMockupContext _context;

    private readonly ISieveProcessor _sieveProcessor;

    public UnitOfWork(RedditMockupContext context, ISieveProcessor sieveProcessor)
    {
        _context = context;
        _sieveProcessor = sieveProcessor;
    }

    public AnswerRepository AnswerRepository =>
        _answerRepository ??= new AnswerRepository(_context, _sieveProcessor);

    public PersonRepository PersonRepository =>
        _personRepository ??= new PersonRepository(_context, _sieveProcessor);

    public ProfileRepository ProfileRepository =>
        _profileRepository ??= new ProfileRepository(_context, _sieveProcessor);

    public QuestionRepository QuestionRepository =>
        _questionRepository ??= new QuestionRepository(_context, _sieveProcessor);

    public RoleRepository RoleRepository =>
        _roleRepository ??= new RoleRepository(_context, _sieveProcessor);

    public UserRepository UserRepository =>
        _userRepository ??= new UserRepository(_context, _sieveProcessor);

    public UserRoleRepository UserRoleRepository =>
        _userRoleRepository ??= new UserRoleRepository(_context, _sieveProcessor);

    public QuestionVoteRepository QuestionVoteRepository =>
        _questionVoteRepository ??= new QuestionVoteRepository(_context, _sieveProcessor);

    public AnswerVoteRepository AnswerVoteRepository =>
        _answerVoteRepository ??= new AnswerVoteRepository(_context, _sieveProcessor);

    public BookmarkRepository BookmarkRepository =>
        _bookmarkRepository ??= new BookmarkRepository(_context, _sieveProcessor);

    public int Commit() =>
        _context.SaveChanges();

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);
}