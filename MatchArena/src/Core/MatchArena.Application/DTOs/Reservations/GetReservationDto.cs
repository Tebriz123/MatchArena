using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Reservations
{
    public record GetReservationDto(
     long Id,
     long FieldId,
     string FieldName,
     TimeOnly ReservedTime,
     DateTime ReservedDate,
     ReservationStatus Status,
     decimal Amount
 );

}
