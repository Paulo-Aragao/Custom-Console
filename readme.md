# Custom Logging for Unity

Este repositório contém um sistema de logging personalizado para Unity, projetado para oferecer mais controle e uma visualização mais clara dos logs diretamente no editor. Ele substitui o console padrão do Unity por uma janela customizada, com recursos de filtragem e uma apresentação mais organizada.

O sistema utiliza o **Sirenix Odin Inspector** para criar uma interface de usuário limpa e funcional.

## ✨ Recursos

-   **Janela de Console Customizada:** Uma janela de editor dockável para visualizar todos os logs do seu projeto.
-   **Categorização de Logs:** Os logs são organizados por níveis: `Debug`, `Gameplay`, `Warning`, `Error`, `Lua`, `Services` e `System`.
-   **Filtros Dinâmicos:** Uma barra de ferramentas com toggles para filtrar facilmente os logs por categoria em tempo real.
-   **Visualização de Detalhes:** Clique em uma entrada de log para ver a mensagem completa e a stack trace (para erros) em uma área de detalhes separada.
-   **Logs Coloridos:** Cada nível de log tem uma cor distinta para facilitar a identificação rápida.
-   **Botão de Limpeza:** Um botão "Clear" para limpar o console a qualquer momento.
-   **Fácil Integração:** Use a classe estática `Logging.Print()` em qualquer lugar do seu código para enviar mensagens ao console personalizado.

## 🔧 Dependências

-   **Unity Editor:** O sistema foi projetado para rodar dentro do editor do Unity.
-   **Sirenix Odin Inspector:** A interface da janela do console é construída com Odin Inspector. Você precisa ter este asset importado no seu projeto.

## ⚙️ Como Usar

### 1. Instalação

1.  Certifique-se de que o **Odin Inspector** esteja instalado no seu projeto Unity.
2.  Copie os scripts `Logging.cs` e `CustomConsoleWindow.cs` para a pasta `Assets` do seu projeto. É recomendado colocá-los dentro de uma pasta `Editor` para que o `CustomConsoleWindow.cs` seja compilado corretamente.

### 2. Abrindo o Console

Para abrir a janela do console personalizado, vá para o menu do Unity e selecione **Window > Custom Logging Console**.

### 3. Enviando Logs

Para enviar mensagens para o novo console, utilize a classe estática `Logging`. Chame o método `Logging.Print()` de qualquer script do seu projeto para registrar um log. O console customizado receberá a mensagem através do evento `OnLogEmitted`.

#### Exemplos de Uso

Aqui estão alguns exemplos de como registrar diferentes tipos de logs:

**Log de Gameplay:**
```csharp
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        Logging.Print("Player Controller inicializado.", Logging.LogLevel.Gameplay);
    }

    void Jump()
    {
        // Lógica do pulo
        Logging.Print("Player pulou!", Logging.LogLevel.Gameplay);
    }
}
```