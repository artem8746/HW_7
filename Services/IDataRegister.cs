using ToDoListWebAPI_HW.Models;

namespace ToDoListWebAPI_HW.Services
{
    public interface IDataRegister
    {
        ITarget Add(ITarget target);
        ITarget ChangeTarget(ChangeTargetRequest request, int id);
        ITarget DeleteTarget(int id);
        ITarget GetTarget(int id);
        List<ITarget> GetTargets();
    }
}