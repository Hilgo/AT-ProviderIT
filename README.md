# AT-ProviderIT

# Gerenciamento de Tarefas

Este é um projeto de gerenciamento de tarefas que permite aos usuários criar, atualizar, excluir e visualizar tarefas.

## Funcionalidades Principais

- Criação de tarefas com título, descrição e data de vencimento.
- Atualização de tarefas existentes.
- Exclusão de tarefas.
- Listagem de todas as tarefas.

## Instruções de Instalação

1. Clone este repositório: `git clone https://github.com/Hilgo/AT-ProviderIT.git`
2. Instale as dependências: `npm install` ou `yarn install`
3. Execute o servidor local: `npm start` ou `yarn start`

## Exemplos de Uso

1. Crie uma nova tarefa:
```
POST /api/Tarefa {
  "Titulo": "Testar Código",
  "Descricao": "Criar teste unitário",
  "DataVencimento": "2024-07-29T19:42:38.680Z",
  "UsuarioAssocioadoId": "string",
  "Status": "Pendente"
}
```

2. Atualize uma tarefa existente:
```
PUT /api/Tarefa/taskId  {
  "Titulo": "Testar Código",
  "Descricao": "Criar teste unitário",
  "DataVencimento": "2024-07-29T19:44:28.875Z",
  "Status": "EmProgresso"
}
```

3. Liste todas as tarefas:
```
GET /api/Tarefa
```


