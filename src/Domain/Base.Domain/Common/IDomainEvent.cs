﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Base.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
