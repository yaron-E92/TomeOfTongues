# ADR 0011: Technical and Display Naming Separation

- Status: Accepted
- Date: 2026-07-23

## Context

Branding may change while assemblies, namespaces, package IDs, data, and
integration identifiers require stability.

## Decision

Use `TomeOfTongues` for the repository, solution, namespaces, assemblies,
projects, paths, pack-engine identifiers, and internal module name. Use
**Tome of Many Tongues** only for user-facing title, UI copy, store metadata,
and the SecondBrain display descriptor.

The initial application ID is `com.tomeoftongues.app` and is documented
separately from the display name.

## Consequences

Display branding can change without assembly or data migrations. Automated
naming checks prevent accidental display-name use in technical identifiers.

## Rejected alternatives

- Spaces in technical identifiers.
- Renaming assemblies when branding changes.
