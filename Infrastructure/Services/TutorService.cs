using AutoMapper;
namespace Infrastructure.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

public class TutorService : ITutorService
{
    private DataContext _context;
    private IMapper _mapper;

    public TutorService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<Tutor>>> GetTutors()
    {
        var get = await _context.Tutors.ToListAsync();
        // var mapped = _mapper.Map<List<Tutor>>(get);
        return new Response<List<Tutor>>(get);
    }

    public async Task<Response<Tutor>> GetTutorById(int id)
    {
        var find = await _context.Tutors.FindAsync(id);
        if(find == null)
        {
            return null;
        }
        // var mapped = _mapper.Map<Tutor>(find);
        return new Response<Tutor>(find);
    }
    public async Task<Response<Tutor>> AddTutor(Tutor dto)
    {

        try
        {
            // var mapped = _mapper.Map<Tutor>(dto);
            await _context.AddAsync(dto);
            await _context.SaveChangesAsync();
            return new Response<Tutor>(dto);
            // _mapper.Map<Tutor>(mapped)

        }
        catch (Exception ex)
        {
            return new Response<Tutor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<Tutor>> UpdateTutor(Tutor dto)
    {
        try
        {
            var mapped = _mapper.Map<Tutor>(dto);
            _context.Attach(mapped);
            _context.Entry(mapped).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            return new Response<Tutor>(dto);

        }
        catch (Exception ex)
        {
            return new Response<Tutor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }

    }

    public async Task<Response<bool>> DeleteTutor(int id)
    {
        try
        {
            var find = await _context.Tutors.FindAsync(id);
            _context.Tutors.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
        }

    }


}
