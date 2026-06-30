🚀 Cenários motivadores
1. Usuário desistindo da operação
Exemplo: Upload ou download de arquivo pesado.

Motivador: Evitar gastar banda e CPU em algo que não será aproveitado.

Boa prática: Sempre propagar o CancellationToken até os métodos mais internos.

2. Timeout controlado
Exemplo: Chamada a serviço externo que pode travar.

Motivador: Garantir que a aplicação não fique bloqueada indefinidamente.

Boa prática: Usar CancellationTokenSource.CancelAfter() para impor limites de tempo.

3. Encerramento da aplicação
Exemplo: API sendo desligada pelo Kubernetes ou IIS.

Motivador: Encerrar tarefas em andamento de forma limpa, liberando recursos.

Boa prática: Usar o IHostApplicationLifetime.ApplicationStopping para disparar cancelamento.

4. Operações concorrentes
Exemplo: Várias tasks em paralelo (ex.: sincronização de dados).

Motivador: Se uma falhar ou o usuário cancelar, todas devem parar.

Boa prática: Compartilhar um mesmo CancellationTokenSource entre tasks.

5. Integração com UI
Exemplo: Usuário clica em “Cancelar” em uma tela de processamento.

Motivador: Evitar travamento da interface e dar feedback imediato.

Boa prática: Conectar o botão de UI ao cts.Cancel() e propagar o token.

🧩 Boas práticas para destacar
Cancelamento é cooperativo: O servidor só para se o código verificar o token.

Tratar OperationCanceledException: Não como erro, mas como fluxo esperado.

Propagação: Sempre passar o token para métodos assíncronos (async/await).

Liberar recursos cedo: Cancelar evita consumo desnecessário de memória, CPU e IO.

Documentar comportamento: O cliente precisa saber se o cancelamento é respeitado ou não.

--------------------
🔎 Como funciona na prática
Escopo por requisição:  
O ASP.NET Core cria um CancellationToken específico para cada request. Esse token é disparado quando o cliente fecha a conexão ou cancela a chamada.
👉 Isso significa que o cancelamento é local àquela requisição.

Independência entre requests:  
Se você tiver duas chamadas simultâneas (ex.: /processaA e /processaB), cancelar uma não interfere na outra. Cada pipeline de request tem seu próprio token.

Exceção:  
Só haveria impacto em múltiplas requisições se você compartilhar manualmente o mesmo CancellationTokenSource entre elas. Isso não é o padrão da API, mas pode ser feito em cenários específicos (ex.: cancelar em lote).

🧩 Exemplos de processos em API REST que usam cancelamento
Uploads/Downloads grandes: Se o cliente fecha a aba, o token dispara e o servidor pode parar de enviar/receber dados.

Consultas demoradas: Uma query pesada no banco pode ser interrompida se o cliente desistir.

Integrações externas: Se o cliente cancela, você pode abortar chamadas a serviços externos para não gastar recursos.

Timeouts: O próprio servidor pode cancelar se a operação passar do tempo limite configurado.

-----------------------

Montar api para treinamento de Cancellation token em branco, que funcione, mas sem mensão ao Cancelattion, token.
Explicar comportamento de Cancelattion Token para o Cliente.

Quando um cliente executaria um cancellationtoken?
Quais processos em api rest que exigem cancellation?
Utilizar o Postman ou Insominia para exemplificar um Cancellation.

1 - Cancelar sem que nada na aplicação valide o IsCancellationRequested na UseCase
2 - Cancelar com o IsCancellationRequested na UseCase retornando mensagem.
3 - Cancelar passando o token para  o Refit chamando a api mock. 
    Mostrar Exception ao chamar o Refit com Refit.ApiRequestException: 'The operation was canceled.'
    Mas a api Mock continua executando.
    A api Mock recebe o cancelation, mas não faz nada porque não há tratamento.

4 - Mostrar api Mock recebendo o CancelattionToken e gerando OperationCanceledException
5 - Mostrar api Mock validando IsCancellationRequested
5.1 Mostrar o ThrowIfCancellationRequested
6 - Na api principal, mostrar o CancellationToken acontecendo no Timeout do HttpClient
    - Mostrar que ao gerar o Cancellation, se não trata com IsCancellationRequested, gera OperationCanceledException
    - Se não tratar o no código principal, ele quebra
    - Adicionar Middleware para tratar e comunicar corretamente o cliente


4 - na api principal, usar o CancellationTokenSource.CancelAfter()