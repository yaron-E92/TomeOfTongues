# ADR 0003: Versioned `.totlang` Package Format

- Status: Accepted
- Date: 2026-07-23

## Context

Curriculum, representations, audio, provenance, and compatibility need a
portable offline format.

## Decision

Use a ZIP-compatible `.totlang` artifact with a versioned JSON manifest,
course/lesson documents, assets, source ledger, license notices, and checksums.
The manifest records minimum engine version and declares only allow-listed
generic capabilities. Packs contain no executable assemblies.

Stable content IDs and per-item revisions preserve learner references across
updates.

## Consequences

Packs can be validated before installation, installed offline, backed up, and
updated independently. Schema migrations and compatibility windows must be
explicit.

## Rejected alternatives

- Database files as distribution packages.
- Unversioned loose JSON.
- Executable language extensions.
