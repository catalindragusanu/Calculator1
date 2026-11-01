# Calculator

A small Windows desktop calculator built with C# and WinForms on .NET 8.

## Features

- Exact `decimal` arithmetic: `0.1 + 0.2` is `0.3`, and integers up to 16 digits keep every digit.
- Pending operation shown above the display (for example `8 ÷`).
- Clear error messages for division by zero and out-of-range results.
- Full keyboard support and screen-reader names on every button.

## Keyboard shortcuts

| Key | Action |
|---|---|
| `0`–`9`, `.` or `,` | Enter digits |
| `+` `-` `*` `/` | Operators |
| `Enter` or `=` | Compute |
| `Backspace` | Delete last digit |
| `Esc` or `Delete` | Clear |

## Behavior

- Operators chain left to right without precedence: `2 + 3 * 4 =` is `20`.
- Pressing an operator twice replaces the pending one.
- Pressing `=` with no pending operation does nothing.
- Results that overflow `decimal`, or underflow to zero, show "Result out of range".

## Project layout

| Project | Purpose |
|---|---|
| `Calculator.Core` | UI-independent arithmetic (`Calculator`) and the key-driven state machine (`CalculatorEngine`) |
| `Calculator` | WinForms shell: keypad layout and keyboard mapping |
| `Calculator.Core.Test` | MSTest suite for the engine and the form wiring |

## Build, run and test

Requires Windows and the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
dotnet build
dotnet test
dotnet run --project Calculator
```
