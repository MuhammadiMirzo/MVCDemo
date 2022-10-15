using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services;

public interface ITutorService
{
    Task<Response<List<Tutor>>> GetTutors();
    Task<Response<Tutor>> GetTutorById(int id);
    Task<Response<Tutor>> AddTutor(Tutor dto);
    Task<Response<Tutor>> UpdateTutor(Tutor dto);
    Task<Response<bool>> DeleteTutor(int id);



}