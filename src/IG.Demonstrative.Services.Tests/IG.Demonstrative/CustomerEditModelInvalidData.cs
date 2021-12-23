using System.Collections;
using System.Collections.Generic;

namespace IG.Demonstrative
{
    public class CustomerEditModelInvalidData : IEnumerable<object[]>
    {
        private const string bigString = "12345678901234567890123456789012345678901234567890123456789012345";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {null, "O campo 'Nome' é obrigatório"};
            yield return new object[] {"", "O campo 'Nome' é obrigatório"};
            yield return new object[] {bigString, "O campo 'Nome' pode ter, no máximo, 64 caracteres" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
