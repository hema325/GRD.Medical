namespace Application.Authors.Queries.GetAuthorById
{
    internal class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new NotFoundException(nameof(Author));

            return _mapper.Map<AuthorDto>(author);
        }
    }
}
