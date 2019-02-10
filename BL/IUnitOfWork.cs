using System;
using System.Collections.Generic;
using System.Text;
using Repositories;
using Domain.Models;

namespace BL
{
    public interface IUnitOfWork
    {
        IRepositoryBase<School> schoolRepository { get; }
        IRepositoryBase<Course> courseRepository { get; }
        IRepositoryBase<Grade> gradeRepository { get; }
        IRepositoryBase<Section> sectionRepository { get; }
        IRepositoryBase<Student> studentRepository { get; }
        void Save();
    }
}
