# AppDemoClientes

Aplicación de escritorio (WinForms, .NET 10) para el mantenimiento de clientes y proveedores. No accede a la base de datos directamente: todo pasa por la API REST [ClientesApi](https://github.com/CarlosEvCode/ClientesApi).

---

## Arquitectura

La solución está dividida en 4 proyectos con dependencias en una sola dirección:

```
UI  →  BOL  →  DAL  →  API REST (ClientesApi)  →  SQL Server
        ↓       ↓
     Entidades (compartida)
```

| Proyecto | Responsabilidad |
|---|---|
| `Entidades` | Modelos de datos (`Cliente`, `Proveedor`). No depende de nadie. |
| `DAL` | Acceso a datos vía `HttpClient` (`ClienteDal`, `ProveedorDal`). |
| `BOL` | Reglas de negocio y validaciones (`ClienteBol`, `ProveedorBol`). |
| `UI` | Formularios WinForms. Solo conversa con BOL. |

Dependencias entre proyectos:

- `UI` → `BOL`, `DAL`, `Entidades`
- `BOL` → `DAL`, `Entidades`
- `DAL` → `Entidades`

---

## Estructura

```
AppDemoClientes/
├── Entidades/
├── DAL/
├── BOL/
└── UI/
    ├── FrmMain.cs
    ├── FrmCliente.cs
    └── FrmProveedor.cs
```

---

## Funcionamiento

Al iniciar se abre `FrmMain`, con dos accesos:

- **Clientes** → `FrmCliente` (listar, buscar, registrar, actualizar, eliminar).
- **Proveedores** → `FrmProveedor` (mismo mantenimiento).

Cada formulario trabaja así:

1. Al abrirse carga la lista desde la API.
2. El buscador filtra por los campos definidos en cada mantenimiento.
3. Al seleccionar una fila, los datos pasan al formulario y se habilita la actualización.
4. **Nuevo** limpia el formulario para registrar un registro nuevo.
5. **Guardar** valida en BOL y luego crea o actualiza en la API.
6. **Eliminar** pide confirmación y borra el registro seleccionado.

Los errores de validación se muestran por campo, y los errores de servidor, timeout o falta de conexión se informan con un mensaje.

---

## Conexión con ClientesApi

`Program.cs` crea un único `HttpClient` con la dirección de la API:

```csharp
BaseAddress = new Uri("https://localhost:7138/")
```

Endpoints utilizados:

- `api/clientes`
- `api/proveedores`

---

## Requisitos

- Windows con .NET 10 (WinForms).
- La API [ClientesApi](https://github.com/CarlosEvCode/ClientesApi) en ejecución.
