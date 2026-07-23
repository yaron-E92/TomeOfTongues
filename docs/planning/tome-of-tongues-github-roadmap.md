# TomeOfTongues GitHub Roadmap

- Status: Created and verified
- Date: 2026-07-23
- Repository: `yaron-E92/TomeOfTongues`
- Architecture: `docs/tome-of-tongues-architecture.md`

## Repository inspection

Immediately before this dry run, TomeOfTongues had nine GitHub default labels,
no milestones, and no issues. There were no duplicates or reusable issues.
SecondBrain's exact shared label descriptions and colors were inspected and are
reused below. No `area:fullstack`, `area:web`, or `area:python` label is needed.

## Creation result

- Labels: 29 created; 38 total including the nine retained defaults.
- Milestones: 10 created.
- Issues: 46 created, numbered `#1` through `#46`.
- Native dependency edges: 116, verified through both `blocked_by` and
  `blocking` endpoints.
- Textual dependencies: every issue body contains actual GitHub issue links for
  `Blocked by` and `Enables`.
- Automation readiness: only `#1` is currently unblocked and labelled
  `autodev:ready` and `codex:ready`.
- Duplicates/reuse: none.
- Execution-state labels on planned issues: none.
- Cross-repository writes: none.

Issue numbers correspond to inventory order: `F01` is
[#1](https://github.com/yaron-E92/TomeOfTongues/issues/1), `F02` is `#2`, and
so on through `H05` as
[#46](https://github.com/yaron-E92/TomeOfTongues/issues/46).

## Labels

Retain the default labels. Create:

| Label group | Labels |
| --- | --- |
| Areas | `area:backend`, `area:maui` |
| Architecture/capability | `architecture`, `clean-architecture`, `domain`, `application`, `content`, `language-pack`, `japanese`, `persistence`, `presentation`, `integration`, `privacy`, `testing`, `statistics`, `ux`, `tomeoftongues` |
| Priority | `priority:high`, `priority:medium`, `priority:low` |
| AutoDev | `autodev:ready`, `autodev:running`, `autodev:blocked`, `autodev:failed`, `autodev:done` |
| Codex | `codex:ready`, `codex:in-progress`, `codex:blocked`, `codex:ready-for-review` |

Only an issue with no unresolved blockers receives `autodev:ready` and
`codex:ready`. Execution-state labels are never assigned during planning.

## Milestones

| Key | Milestone | Outcome and completion |
| --- | --- | --- |
| M0 | TomeOfTongues Architecture & Repository Foundation | A .NET 10 solution, enforced dependency graph, reproducible non-MAUI/Android verification, and no product behavior. Complete when structure and CI checks pass. |
| M1 | Generic Language Pack Platform | Any data-only `.totlang` pack can be compiled, rights-validated, installed, and compatibility-checked without a language code dependency. Excludes Japanese curriculum authoring. Depends on M0. |
| M2 | Core Learning Domain | Generic curriculum, sessions, exercises, evidence, skill/progress state, and application ports are independently tested. Excludes persistence and UI. Depends on M0. |
| M3 | Japanese Starter Course & Audio | A reviewed Japanese pack contains six original practical lessons, at least twenty rights-cleared audio prompts, and passing artifact/flow validation. Excludes copied courses and complete A1/A2. Depends on M1 and M2 contracts. |
| M4 | Local Progress & Review | SQLite safely persists sessions, progress, attempts, review, preferences, and deferred speaking with recoverable migrations. Excludes cloud sync. Depends on M2. |
| M5 | Contextual Assistance & Silent Learning | Assistance, script familiarity, error attribution, Silent Mode, and indefinite speaking deferral are generic and tested. Excludes speech recognition and handwriting. Depends on M1 and M2. |
| M6 | MAUI Usable Learning Experience | The generic Android-first MAUI host can select an installed pack and complete Today, Lesson, Review, Settings, and Speaking Queue flows. Contains no language reference. Depends on M1–M5 vertical-slice contracts. |
| M7 | Insights, Import & Portability | Evidence-based insights, private import, sources/legal UI, backup, export, restore, and recovery are usable locally. Excludes accounts and cloud sync. Depends on M4 and M6. |
| M8 | SecondBrain Integration | A human-approved host contract and opt-in adapter expose redacted generic recommendations/completions without database access. Excludes cross-repository changes without authorization. Depends on stable M2, M4, and M7 read models. |
| M9 | MVP Hardening | Accessibility, recovery, privacy, licensing, offline, Android release, and non-Japanese architecture assumptions are verified. Excludes new product capabilities. Depends on the affected earlier milestones. |

## Canonical issue body

Every issue created from this plan uses these sections:

1. `# Summary` — the concrete outcome from the inventory below.
2. `## Context` — architecture and applicable ADR links.
3. `## Scope` — the listed deliverable only.
4. `## Out of Scope` — production concerns assigned to other inventory items,
   plus no backend/cloud, no unrelated refactor, no language-project reverse
   dependency, and no protected-content copying.
5. `## Acceptance Criteria` — implementation exists, focused tests pass,
   architecture boundaries remain valid, and the outcome is documented.
6. `## Technical Notes` — named layer/capability, data ownership, and dependency
   direction from the architecture.
7. `## Dependencies` — actual GitHub issue numbers for `Blocked by`, `Enables`,
   and related-but-not-blocking.
8. `## Verification` — targeted project commands; backend work uses
   `TomeOfTongues.NonMaui.slnf`, MAUI work builds its `.csproj`, pack work uses
   `TomeOfTongues.Content.Tool`.
9. `## Documentation` — architecture/ADR/content/source documentation affected.
10. `## Automation Notes` — suitability, risks, human review, and forbidden
    files/layers.

## Issue inventory and dependencies

Logical IDs are replaced by actual issue numbers during creation. Every
`Blocked by` edge is written in the issue body and added through GitHub's native
issue-dependencies API.

| ID | Milestone | Issue title | Blocked by | Primary labels | Automation / human review |
| --- | --- | --- | --- | --- | --- |
| F01 | M0 | Create the .NET 10 TomeOfTongues solution and project structure | None | `architecture`, `clean-architecture`, `area:backend`, `priority:high` | Ready; no special review |
| F02 | M0 | Add shared SDK, build settings, and non-MAUI solution filter | F01 | `architecture`, `clean-architecture`, `area:backend`, `priority:high` | Suitable after blocker |
| F03 | M0 | Enforce project and language-pack dependency boundaries | F01, F02 | `architecture`, `clean-architecture`, `testing`, `area:backend`, `priority:high` | Suitable after blockers |
| F04 | M0 | Add split non-MAUI and Android MAUI CI verification | F02, F03 | `architecture`, `testing`, `area:backend`, `area:maui`, `priority:high` | Suitable after blockers |
| P01 | M1 | Define the versioned `.totlang` schema and representation model | F01, F02 | `content`, `language-pack`, `domain`, `area:backend`, `priority:high` | Suitable after blockers |
| P02 | M1 | Implement generic language-pack compile and validate tooling | P01 | `content`, `language-pack`, `area:backend`, `priority:high` | Suitable after blocker |
| P03 | M1 | Implement runtime language-pack discovery and installation | P01, P02 | `content`, `language-pack`, `application`, `area:backend`, `priority:high` | Suitable after blockers |
| P04 | M1 | Enforce content provenance, attribution, checksums, and rights | P01, P02 | `content`, `language-pack`, `privacy`, `area:backend`, `priority:high` | Suitable; legal metadata review |
| P05 | M1 | Implement language-pack version and compatibility handling | P01, P03 | `content`, `language-pack`, `application`, `area:backend`, `priority:medium` | Suitable after blockers |
| C01 | M2 | Implement curriculum and resumable learning-session models | F01, F02 | `domain`, `area:backend`, `priority:high` | Suitable after blockers |
| C02 | M2 | Implement exercises, attempts, and multidimensional evidence | C01 | `domain`, `area:backend`, `priority:high` | Suitable after blocker |
| C03 | M2 | Implement learner skill and curriculum-progress states | C01, C02 | `domain`, `area:backend`, `priority:high` | Suitable after blockers |
| C04 | M2 | Add application learning use cases, ports, and transactions | C01, C02, C03 | `application`, `area:backend`, `priority:high` | Suitable after blockers |
| J01 | M3 | Establish the Japanese pack source structure and rights ledger | P02, P04 | `content`, `language-pack`, `japanese`, `privacy`, `area:backend`, `priority:high` | Suitable; content-owner review |
| J02 | M3 | Author six original practical Japanese starter lessons | J01, C01 | `content`, `japanese`, `area:backend`, `priority:high` | Human linguistic review required |
| J03 | M3 | Produce rights-cleared human audio for the Japanese starter pack | J01 | `content`, `japanese`, `privacy`, `priority:high` | Human contributor required; withhold automation |
| J04 | M3 | Review Japanese language, audio, readings, rōmaji, and licenses | J02, J03 | `content`, `japanese`, `privacy`, `testing`, `priority:high` | Human reviewer required; withhold automation |
| J05 | M3 | Add Japanese artifact-validation and learning-flow tests | J04, P03, C04 | `content`, `language-pack`, `japanese`, `testing`, `area:backend`, `priority:high` | Suitable after review |
| L01 | M4 | Add SQLite migration, integrity, backup, and recovery infrastructure | F02, C04 | `persistence`, `privacy`, `area:backend`, `priority:high` | Suitable after blockers |
| L02 | M4 | Persist learning sessions and curriculum progress | L01, C04 | `persistence`, `application`, `area:backend`, `priority:high` | Suitable after blockers |
| L03 | M4 | Persist attempts and evidence observations | L01, C02 | `persistence`, `domain`, `area:backend`, `priority:high` | Suitable after blockers |
| L04 | M4 | Implement and persist review scheduling | C02, C03, L01, L03 | `domain`, `persistence`, `area:backend`, `priority:high` | Suitable after blockers |
| L05 | M4 | Persist learner preferences and deferred-speaking state | L01, C04 | `persistence`, `privacy`, `area:backend`, `priority:high` | Suitable after blockers |
| A01 | M5 | Implement generic assistance modes and adaptive restoration | P01, C02, C03 | `domain`, `language-pack`, `area:backend`, `priority:high` | Suitable after blockers |
| A02 | M5 | Track representation familiarity and attribute modality mistakes | P01, C02, C03 | `domain`, `statistics`, `area:backend`, `priority:high` | Suitable after blockers |
| A03 | M5 | Implement Silent Mode and non-blocking deferred-speaking rules | C01, C02, C04 | `domain`, `privacy`, `area:backend`, `priority:high` | Suitable after blockers |
| U01 | M6 | Add generic MAUI Shell and installed-pack selection | P03, L01 | `presentation`, `ux`, `area:maui`, `priority:high` | Suitable after blockers; UI review |
| U02 | M6 | Add Today and resumable lesson flows | U01, C04, L02, J02 | `presentation`, `ux`, `area:maui`, `priority:high` | Suitable after blockers; UI review |
| U03 | M6 | Add generic exercise and audio controls | U02, J03, C02 | `presentation`, `ux`, `area:maui`, `priority:high` | Suitable after blockers; device/audio review |
| U04 | M6 | Add the MAUI review flow | U01, L04 | `presentation`, `ux`, `area:maui`, `priority:high` | Suitable after blockers |
| U05 | M6 | Add assistance and Silent Mode settings | U01, A01, A02, L05 | `presentation`, `privacy`, `ux`, `area:maui`, `priority:high` | Suitable after blockers |
| U06 | M6 | Add the deferred-speaking queue UI | U02, A03, L05 | `presentation`, `privacy`, `ux`, `area:maui`, `priority:high` | Suitable after blockers |
| I01 | M7 | Add evidence-based statistics projections | L03, L04, A02 | `statistics`, `application`, `area:backend`, `priority:medium` | Suitable after blockers |
| I02 | M7 | Add progress and review-health UI | I01, U01 | `statistics`, `presentation`, `ux`, `area:maui`, `priority:medium` | Suitable after blockers |
| I03 | M7 | Add private language-pack and content import | P03, P04, L01 | `content`, `privacy`, `application`, `area:backend`, `area:maui`, `priority:medium` | Suitable; privacy review |
| I04 | M7 | Add backup, export, restore, and failure recovery | L01, L02, L03 | `persistence`, `privacy`, `area:backend`, `area:maui`, `priority:medium` | Suitable; destructive-path review |
| I05 | M7 | Add source, attribution, privacy, and legal UI | P04, U01, J04 | `content`, `privacy`, `presentation`, `area:maui`, `priority:medium` | Suitable after human content review |
| S01 | M8 | Add language-neutral learning integration read models | C04, I01 | `application`, `integration`, `area:backend`, `priority:low` | Suitable after blockers |
| S02 | M8 | Coordinate and freeze SecondBrain learning-provider contracts | S01 | `architecture`, `integration`, `documentation`, `priority:low` | Human approval; withhold automation |
| S03 | M8 | Implement opt-in SecondBrain adapter and module registration | S02, P03, L01 | `integration`, `privacy`, `area:backend`, `priority:low` | Suitable after contract approval |
| S04 | M8 | Add SecondBrain integration and privacy-boundary tests | S03 | `integration`, `privacy`, `testing`, `area:backend`, `priority:low` | Suitable after blocker |
| H01 | M9 | Review accessibility and notification privacy | U02, U03, U04, U05, U06 | `ux`, `privacy`, `testing`, `area:maui`, `priority:medium` | Human accessibility review |
| H02 | M9 | Test migrations, corruption recovery, and interrupted imports | L01, L02, L03, I04 | `persistence`, `privacy`, `testing`, `area:backend`, `priority:high` | Suitable; destructive-path review |
| H03 | M9 | Audit offline behavior, licenses, assets, and private data | J04, I03, I05 | `privacy`, `content`, `testing`, `area:backend`, `area:maui`, `priority:high` | Human legal/privacy review |
| H04 | M9 | Add reproducible Android release-build and smoke verification | F04, J05, U03, U04, U05, U06, H01, H02, H03 | `testing`, `presentation`, `area:maui`, `priority:high` | Suitable after all MVP gates |
| H05 | M9 | Validate generic representations with a Hebrew-shaped fixture | P01, A01, A02 | `architecture`, `language-pack`, `testing`, `area:backend`, `priority:low` | Suitable; no production Hebrew |

## Issue-specific acceptance and verification rules

The title and inventory row define the single implementation outcome. Each
issue body expands acceptance with these category rules:

- Foundation: solution metadata is deterministic; architecture/CI checks detect
  a representative failure; no product behavior is added.
- Pack platform: malformed or incompatible packs fail safely; no executable
  assembly or language type can load; valid packs round-trip deterministically.
- Core/Application: pure non-MAUI tests cover invariants, cancellation, failure,
  and the separation of completion, evidence, and scheduling.
- Japanese content: only original/reviewed material enters the pack; all
  representations align; every bundled asset has a valid rights record.
- Persistence: temporary real SQLite tests cover restart, transaction rollback,
  migration, integrity, and recovery without silent deletion.
- Assistance/Silent Mode: all modes and restoration paths are tested; script,
  listening, and speaking evidence remain separate; progression never gates.
- MAUI: build the `.csproj` for `net10.0-android`; views consume generic DTOs;
  source scans find no Japanese project/type/filename/tag.
- Insights/import: samples and measurement types are visible; private data is
  excluded by default; import/export is integrity-checked and recoverable.
- SecondBrain: standalone builds without host packages; the adapter has no SQL
  or database path; opt-in and redaction are tested.
- Hardening: audit evidence is recorded and failures are reproducible; the
  Hebrew fixture remains non-production.

## Dependency creation and verification

Issues are created in inventory order. Once real numbers and database IDs are
known, each issue body is patched with Markdown links for `Blocked by` and
`Enables`. Native relationships are then added with:

`POST /repos/yaron-E92/TomeOfTongues/issues/{issue_number}/dependencies/blocked_by`

using the blocking issue's database `issue_id`. Verification lists both
`blocked_by` and `blocking` and compares them with this table.

## Automation policy

F01 is the only initially unblocked implementation issue and receives
`autodev:ready` and `codex:ready`. Dependent issues are automation-suitable
where stated but receive no readiness label until their blockers close. Human
content, audio, accessibility, legal/privacy, and SecondBrain contract gates
are intentionally withheld from autonomous execution.

## Duplicate and cross-repository policy

No duplicates were found. All issues are created in TomeOfTongues. S02 may
produce a proposed SecondBrain contract, but it does not authorize a write to
the SecondBrain repository; any cross-repository issue or code change requires
separate authorization.
