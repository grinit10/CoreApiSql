using Data;
using Domain.Models;
using Repositories;

namespace BL
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly IRepositoryBase<School> _schoolRepository;
        private readonly IRepositoryBase<Course> _courseRepository;
        private readonly IRepositoryBase<Grade> _gradeRepository;
        private readonly IRepositoryBase<Section> _sectionRepository;
        private readonly IRepositoryBase<Student> _studentRepository;
        private ISchoolDbContext RepositoryContext { get; set; }

        public UnitOfWork(IRepositoryBase<School> schoolRepository,
                          IRepositoryBase<Course> courseRepository,
                          IRepositoryBase<Grade> gradeRepository,
                          IRepositoryBase<Section> sectionRepository,
                          IRepositoryBase<Student> studentRepository,
                          ISchoolDbContext repositoryContext)
        {
            _schoolRepository = schoolRepository;
            _courseRepository = courseRepository;
            _gradeRepository = gradeRepository;
            _sectionRepository = sectionRepository;
            _studentRepository = studentRepository;
            RepositoryContext = repositoryContext;
        }

        public IRepositoryBase<School> schoolRepository { get { return _schoolRepository; } }
        public IRepositoryBase<Course> courseRepository { get { return _courseRepository; } }
        public IRepositoryBase<Grade> gradeRepository { get { return _gradeRepository; } }
        public IRepositoryBase<Section> sectionRepository { get { return _sectionRepository; } }
        public IRepositoryBase<Student> studentRepository { get { return _studentRepository; } }

        public void Save() => RepositoryContext.GetSaveChanges();
    }
}
