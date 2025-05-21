using Core.Model;

namespace Core.Interface;

public interface IInstruction : ISemantic
{
    /// <summary>
    /// Hace un recorrido de evaluación en el AST para ejecutar el código
    /// </summary>
    /// <param name="context">Contexto en el que se ejecuta el nodo</param>
    void Evaluate(Context context);
}
