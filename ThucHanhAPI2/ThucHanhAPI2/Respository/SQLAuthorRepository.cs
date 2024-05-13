using ThucHanhAPI2.Model.DTO;
using ThucHanhAPI2.Data;
using ThucHanhAPI2.Model.Domain;
using ThucHanhAPI2.Repositories;

namespace ThucHanhAPI2.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AuthorDTO> GellAllAuthors()
        {
            //Get Data From Database -Domain Model 
            var allAuthorsDomain = _dbContext.Author.ToList();
            //Map domain models to DTOs 
            var allAuthorDTO = new List<AuthorDTO>();
            foreach (var authorDomain in allAuthorsDomain)
            {
                allAuthorDTO.Add(new AuthorDTO()
                {
                    Id = authorDomain.Id,
                    FullName = authorDomain.FullName
                });
            }
            //return DTOs 
            return allAuthorDTO;
        }
        public AuthorNoIdDTO GetAuthorById(int id)
        {
            // get book Domain model from Db
            var authorWithIdDomain = _dbContext.Author.FirstOrDefault(x => x.Id ==
           id);
            if (authorWithIdDomain == null)
            {
                return null;
            }
            //Map Domain Model to DTOs 
            var authorNoIdDTO = new AuthorNoIdDTO
            {
                FullName = authorWithIdDomain.FullName,
            };
            return authorNoIdDTO;
        }
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorDomainModel = new Author
            {
                FullName = addAuthorRequestDTO.FullName,
            };
            //Use Domain Model to create Author 
            _dbContext.Author.Add(authorDomainModel);
            _dbContext.SaveChanges();
            return addAuthorRequestDTO;
        }
        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var authorDomain = _dbContext.Author.FirstOrDefault(n => n.Id == id);
            if (authorDomain != null)
            {
                authorDomain.FullName = authorNoIdDTO.FullName;
                _dbContext.SaveChanges();
            }
            return authorNoIdDTO;
        }
        public Author? DeleteAuthorById(int id)
        {
            var authorDomain = _dbContext.Author.FirstOrDefault(n => n.Id == id);
            if (authorDomain != null)
            {
                _dbContext.Author.Remove(authorDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}
