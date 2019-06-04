using Data;
using Domain.Models;
using Repositories;

namespace BL
{
    public class UnitOfWork: IUnitOfWork
    {
        private ISchoolDbContext RepositoryContext { get; set; }

        public UnitOfWork(IRepositoryBase<School> schoolRepository,
                          IRepositoryBase<Course> courseRepository,
                          IRepositoryBase<Grade> gradeRepository,
                          IRepositoryBase<Section> sectionRepository,
                          IRepositoryBase<Student> studentRepository,
                          ISchoolDbContext repositoryContext)
        {
            this.schoolRepository = schoolRepository;
            this.courseRepository = courseRepository;
            this.gradeRepository = gradeRepository;
            this.sectionRepository = sectionRepository;
            this.studentRepository = studentRepository;
            RepositoryContext = repositoryContext;
        }

        public IRepositoryBase<School> schoolRepository { get; }
        public IRepositoryBase<Course> courseRepository { get; }
        public IRepositoryBase<Grade> gradeRepository { get; }
        public IRepositoryBase<Section> sectionRepository { get; }
        public IRepositoryBase<Student> studentRepository { get; }

        public void Save() => RepositoryContext.GetSaveChanges();
    }
}
