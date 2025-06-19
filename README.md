# Pixel Wall-E Compiler
Pixel Wall-E Compiler es un proyecto desarrollado para la interpretación y compilación de un lenguaje personalizado orientado a la manipulación de imágenes tipo pixel art. El sistema está diseñado para facilitar la creación, edición y análisis de scripts que definen acciones sobre imágenes, integrando componentes de análisis léxico, sintáctico, semántico y generación de código, así como una interfaz gráfica para la visualización y edición.

## Estructura del Proyecto
El proyecto está organizado en módulos independientes que abarcan desde el análisis del lenguaje hasta la interacción visual con el usuario. Cada módulo cumple una función específica dentro del flujo de procesamiento del lenguaje Pixel Wall-E.

## Módulos Principales
- **Core**: Contiene la lógica central, definiciones de interfaces, enumeraciones, modelos de datos y gestión de errores.
- **Lexer**: Implementa el análisis léxico, transformando el código fuente en tokens comprensibles para el parser.
- **Parser**: Realiza el análisis sintáctico, construyendo el árbol de sintaxis abstracta (AST) a partir de los tokens generados por el lexer.
- **Semantic**: Encargado del análisis semántico, validando la coherencia y significado de las instrucciones dentro del contexto del lenguaje.
- **CodeGen**: Responsable de la generación de código o instrucciones ejecutables a partir del AST validado.
- **Visual**: Proporciona la interfaz gráfica de usuario (GUI) para la edición, visualización y manipulación de imágenes y scripts.
- **Test**: Incluye pruebas automatizadas para validar el correcto funcionamiento de los distintos módulos del sistema.

## Características Destacadas
- Análisis léxico, sintáctico y semántico personalizado para el lenguaje Pixel Wall-E.
- Generación de código orientado a la manipulación de imágenes tipo pixel art.
- Interfaz gráfica moderna para la edición visual y textual de scripts.
- Sistema robusto de manejo y reporte de errores.
- Arquitectura modular que facilita la extensión y el mantenimiento del proyecto.

## Uso y Ejecución
El proyecto puede ser compilado y ejecutado desde Visual Studio o mediante CLI utilizando .NET. Se recomienda revisar la documentación interna de cada módulo para detalles específicos de uso y extensión.

## Licencia
Este proyecto se distribuye bajo la licencia especificada en el archivo LICENSE.
