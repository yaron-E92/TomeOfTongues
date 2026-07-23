# ADR 0001: Generic Engine and Declarative Language Packs

- Status: Accepted
- Date: 2026-07-23

## Context

Japanese is the first language, but the learning engine must not become
Japanese-specific or a speculative universal linguistics framework.

## Decision

Build a generic learning engine around curriculum, sessions, evidence, review,
representations, and assistance. Languages are versioned declarative `.totlang`
artifacts. The Japanese authoring project emits data rather than a runtime
extension assembly.

## Consequences

Japanese knowledge stays in its pack, mobile AOT does not require dynamic code
loading, and future languages reuse proven capabilities. Language behavior that
cannot be represented declaratively requires a later ADR backed by a concrete
second-language need.

## Rejected alternatives

- Japanese-specific application: leaks first-language assumptions everywhere.
- Universal linguistic framework: generalizes before evidence exists.
- Executable plug-ins: complicate trimming, AOT, packaging, and trust.
