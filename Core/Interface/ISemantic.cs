using Core.Model;

namespace Core.Interface;

public interface ISemantic
{
    /// <summary>
    /// Hace un recorrido de Checkeo Semantico en el AST para ejecutar el código
    /// </summary>
    /// <param name="context">Contexto en el que se ejecuta el nodo</param>
    /// <returns>Devuelve si está correcto el nodo asociado</returns>
    bool CheckSemantic(Context context);
}

