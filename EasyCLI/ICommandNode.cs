using System.Collections;
using System.Collections.Generic;

namespace EasyCli
{
    internal interface ICommandNode: ICollection<ICommand>, IEnumerable<ICommand>, IReadOnlyCollection<ICommand>, ICollection
    {
    }
}