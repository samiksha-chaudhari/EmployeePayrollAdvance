using EmpModel;

namespace EmpRepository.Interface
{
    public interface IEmployeeRepository
    {
        bool Register(EmployeeModel EmployeeData);
    }
}