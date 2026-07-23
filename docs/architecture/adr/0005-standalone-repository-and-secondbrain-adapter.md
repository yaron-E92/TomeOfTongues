# ADR 0005: Standalone Repository and SecondBrain Adapter Boundary

- Status: Accepted
- Date: 2026-07-23

## Context

TomeOfTongues must remain independently useful while participating in the
SecondBrain app family.

## Decision

Keep TomeOfTongues in its standalone repository with its own MAUI composition
root and data. A post-MVP `TomeOfTongues.Integration.SecondBrain` adapter maps
language-neutral Application read models into host-owned contracts.

SecondBrain never queries Tome SQLite, references language projects, or owns
learning state. Standalone and embedded installations use separate local
stores; transfer is explicit export/import.

## Consequences

Standalone builds and releases remain possible. Integration requires a stable,
versioned `SecondBrain.Abstractions` contract and explicit opt-in.

## Rejected alternatives

- Making TomeOfTongues a SecondBrain-only module.
- Shared cross-app database access.
- SecondBrain types inside Tome Core or Application.
