//Интерфейс, который будет реализован в форме, чтобы передавать данные между Presenter и View.

using System;
using System.Collections.Generic;

namespace Labs8C_
{
    public interface IStudentView
    {
        string StudentName { get; }
        string RecordBook { get; }
        string GroupNumber { get; }
        string Department { get; }
        string Specification { get; }
        DateTime DateOfAdmission { get; }
        string SelectedStudentRecordBook { get; }


        void SetStudents(List<Student> students);
    }
}
