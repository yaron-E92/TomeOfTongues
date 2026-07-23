# ADR 0009: Silent Mode and Deferred Speaking

- Status: Accepted
- Date: 2026-07-23

## Context

Learners cannot always play audio or use a microphone, and speech recognition
is not an MVP dependency.

## Decision

Silent Mode prevents automatic audio and microphone use. Speaking work always
offers silent completion or deferral. Deferral completes the curriculum step
without spoken-production evidence and persists an optional speaking
opportunity indefinitely.

Notifications are off by default and reveal no lesson or imported text.

## Consequences

Speaking never gates progression. Spoken ability remains separately
unobserved/self-reported until valid evidence is recorded.

## Rejected alternatives

- Mandatory speaking checkpoints.
- Treating deferral as lesson failure.
- Automatic microphone activation.
