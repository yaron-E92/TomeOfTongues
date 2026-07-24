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

Schema version 1 is represented by the language-neutral contracts in
`TomeOfTongues.Content.Schema`. Every JSON document declares
`"schemaVersion": 1`, uses camel-case property and enum names, rejects unknown
members, and preserves authored text without runtime transliteration or
language-specific code.

The v1 document set contains:

- a manifest with pack/package/engine versions, BCP 47 language and locale
  tags, localized display names, representation definitions, allow-listed
  normalization operations, course IDs, assets, sources, and licenses;
- a course catalog with stable course/unit/lesson IDs and per-item revisions;
- lesson documents with objectives, expressions, steps, and exercises;
- expression representations identified by language-neutral IDs, BCP 47 script
  tags, direction, authored text, range annotations, natural/literal meanings,
  audio references, and source references;
- generic exercise modalities, acceptable answers, assistance groups, scoring
  rules, and evidence mappings.

The contract deliberately has no score-based progression gate. Script
recognition can emit evidence but cannot require a correct result to advance.
Every spoken response and optional speaking opportunity must use
`deferredAllowed`, so speaking may be skipped or completed later without
blocking lesson progress.

This decision defines the document representation and intrinsic schema
invariants only. Package compilation and broad semantic validation, rights and
checksum enforcement, installation, and engine compatibility-window policy
remain separate language-pack platform outcomes.

## Consequences

Packs can be validated before installation, installed offline, backed up, and
updated independently. Schema migrations and compatibility windows must be
explicit.

The generic engine can deserialize declarative data without referencing or
loading a language project or executable assembly. Unsupported versions,
unknown members, malformed JSON, invalid annotation ranges, and gating spoken
work fail at the schema boundary.

## Verification

```powershell
dotnet test tests/TomeOfTongues.Content.Tests/TomeOfTongues.Content.Tests.csproj
dotnet test tests/TomeOfTongues.Architecture.Tests/TomeOfTongues.Architecture.Tests.csproj
```

## Rejected alternatives

- Database files as distribution packages.
- Unversioned loose JSON.
- Executable language extensions.
