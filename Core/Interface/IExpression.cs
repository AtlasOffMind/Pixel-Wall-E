using Core.Model;

namespace Core.Interface;

public interface IExpression<T> //Expresiones que te devuelven algo
{
    /// <summary>
    /// Hace un recorrido de evaluación en el AST para ejecutar el código
    /// </summary>
    /// <param name="context">Contexto en el que se ejecuta el nodo</param>
    /// <returns>Devuelve un valor en base a la definición del nodo</returns>
    T Evaluate(Context context);
}

