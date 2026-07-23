# ADR 0002: No Dependencies on Language-Specific Projects

- Status: Accepted
- Date: 2026-07-23

## Context

Referencing a Japanese project from MAUI, Application, Infrastructure, an
integration, or a generic test would reverse the intended extension direction.

## Decision

No production or generic test project may reference a
`TomeOfTongues.Language.*` project, assembly, namespace, or type. Language
projects may depend inward on generic tooling. Dedicated language tests consume
the built artifact through generic APIs rather than a runtime assembly.

Generic staging discovers projects marked `TomeLanguagePack=true` and copies
their `.totlang` outputs without hard-coded names.

## Consequences

The generic engine remains independently buildable. Architecture tests must
inspect project references, packages, namespaces, MAUI assets, staged packs,
and source identifiers.

## Rejected alternatives

- Direct MAUI-to-Japanese reference.
- A composition project referencing every language.
- Reflection over bundled language assemblies.
