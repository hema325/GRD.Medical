using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalInformation.Commands.UpdateMedicalInfo
{
    public class UpdateMedicalInfoCommandHandler : IRequestHandler<UpdateMedicalInfoCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public UpdateMedicalInfoCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<Unit> Handle(UpdateMedicalInfoCommand request, CancellationToken cancellationToken)
        {
            var medicalInfo = await _context.MedicalInfos.FirstOrDefaultAsync(u => u.UserId == _currentUser.Id);

            if (medicalInfo == null)
            {
                CreateMedicalInfo(request, ref medicalInfo);
                _context.MedicalInfos.Add(medicalInfo);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }

            UpdateMedicalInfo(request, ref medicalInfo);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
        private void CreateMedicalInfo(UpdateMedicalInfoCommand request, ref MedicalInfo medicalInfo)
        {
            medicalInfo = new MedicalInfo
            {
                Age = request.Age,
                Hight = request.Hight,
                Wight = request.Wight,
                Diabetic = request.Diabetic,
                Hypertension = request.Hypertension,
                Hypotension = request.Hypotension,
                Smoker = request.Smoker,
                UserId = _currentUser.Id.Value
            };
            medicalInfo.AddDomainEvent(new EntityCreatedEvent(medicalInfo));
        }
        private void UpdateMedicalInfo(UpdateMedicalInfoCommand request, ref MedicalInfo medicalInfo)
        {
            medicalInfo.Age = request.Age;
            medicalInfo.Hight = request.Hight;
            medicalInfo.Wight = request.Wight;
            medicalInfo.Diabetic = request.Diabetic;
            medicalInfo.Hypertension = request.Hypertension;
            medicalInfo.Hypotension = request.Hypotension;
            medicalInfo.Smoker = request.Smoker;

            medicalInfo.AddDomainEvent(new EntityUpdatedEvent(medicalInfo));
        }
    }
}
