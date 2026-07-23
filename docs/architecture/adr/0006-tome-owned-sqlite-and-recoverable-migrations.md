# ADR 0006: Tome-Owned SQLite and Recoverable Migrations

- Status: Accepted
- Date: 2026-07-23

## Context

Learning state must survive offline use and schema evolution without a backend.

## Decision

Infrastructure exclusively owns `tomeoftongues.db3` and uses
`Microsoft.Data.Sqlite`, explicit SQL repositories, and numbered migrations.
Large pack/audio/import assets remain app-private files.

Before destructive migration, checkpoint WAL, verify integrity, and create a
recoverable backup. Migration failure rolls back and exposes read-only
export/recovery; it never silently resets learner data.

## Consequences

Persistence stays behind Application ports and is independently testable with
temporary real databases. Backup and recovery behavior require integration
tests.

## Rejected alternatives

- Direct database use from MAUI or SecondBrain.
- Automatic delete-and-recreate recovery.
- Mandatory backend persistence.
