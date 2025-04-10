# CurriculumATS API

Esta é uma API em .NET 9 que gera currículos otimizados para sistemas ATS (Applicant Tracking System).

## ✨ Funcionalidades

- Geração de currículo baseado nos dados cadastrados.
- Exportação em formato **HTML**.
- Link para visualização do currículo direto no navegador.

## 🔧 Tecnologias

- ASP.NET Core 9
- MongoDb
- Bootstrap 5 (no HTML gerado)
- Serviço hospedado com arquivos estáticos em `wwwroot`

## 📂 Endpoints principais

### `GET /api/curriculoats/montar-curriculo/{pessoaId}`

Retorna o DTO do currículo em JSON.

### `GET /api/curriculoats/html-salvar/{pessoaId}`

Gera e salva um arquivo HTML com o currículo e retorna a URL pública para visualização.

#### Exemplo de resposta:
```json
{
  "url": "https://localhost:5001/curriculos/curriculo_Fulano_20250410154830.html"
}
```

## 📁 Estrutura esperada

O projeto salva os currículos HTML em:

```
CurriculumATS.API/wwwroot/curriculos/
```

Garanta que o middleware de arquivos estáticos esteja habilitado no `Program.cs`:

```csharp
app.UseStaticFiles();
```

## ✅ Como rodar

1. Compile o projeto com .NET 9.
2. Execute o projeto.
3. Acesse os endpoints via navegador ou Postman.

---

Desenvolvido com 💼 por [Seu Nome].
