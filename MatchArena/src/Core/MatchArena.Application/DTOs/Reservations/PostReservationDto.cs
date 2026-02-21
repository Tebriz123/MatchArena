using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Reservations
{
    public record PostReservationDto(
     long FieldId,
     TimeOnly ReservedTime,
     DateTime ReservedDate
 );

}
