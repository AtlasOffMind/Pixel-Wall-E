using Core.Model;

namespace Core.Interface;

public interface IInstruction
{
    /// <summary>
    /// Hace un recorrido de evaluación en el AST para ejecutar el código
    /// </summary>
    /// <param name="context">Contexto en el que se ejecuta el nodo</param>
    void Evaluate(Context context);
}
