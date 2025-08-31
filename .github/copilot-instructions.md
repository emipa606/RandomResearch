# GitHub Copilot Instructions for Random Research (Continued) Mod

## Mod Overview and Purpose

**Mod Name:** Random Research (Continued)

This mod, originally developed by Uuugggg and updated in this continuation, provides an engaging twist to the research system in RimWorld. The primary goal of the mod is to introduce a scenario option for random research, injecting unpredictability into the research process. A secondary feature is optional blind research, where players are kept in suspense, unaware of the research topic until substantial progress is achieved. This mod can be toggled on as a scenario option, allowing players the choice to keep it active during standard gameplay without randomizing research.

## Key Features and Systems

1. **Random Research:** Engage in a scenario where research topics are chosen at random. This adds uncertainty and strategic depth as players cannot control the research sequence.

2. **Blind Research:** Optionally obscure the identity of the research project until a configurable percentage of progress is reached.

3. **Re-roll Functionality:** A button allows for rerolling the research topic once it is known, providing fluidity to the research process.

4. **Development Mode Integration:** Access custom commands such as toggling random research or setting blind research thresholds through the development mode.

5. **ResearchTree Mod Support:** Prevents queuing and displaying of in-progress research, aligning with the ResearchTree GUI modifications.

## Coding Patterns and Conventions

- Follow Class PascalCase and method camelCase conventions consistently across all source files.
- Employ access modifiers appropriately, using `internal` for within-assembly access and `public` for broader exposure.
- Use static classes efficiently for utility-focused methods and logic encapsulation across several files.
- Pay attention to class-level documentation and summary tags for better code readability.

## XML Integration

- **Def Relationships:** Ensure that XML definitions like "defRandomResearch" are correctly aligned with scenario setups and can be manipulated through save files for adjusting blind research thresholds.
- **Scenario Editor Integration:** Utilize XML to define custom scenario parts such as the "Random Research" option within the Scenario Editor.

## Harmony Patching

- Utilize Harmony to override specific methods in RimWorld, ensuring compatibility and integration with base game functionalities.
- Ensure patches are non-invasive and maintain the mod's independence from core modifiable logic, focusing on adding or modifying rather than replacing existing functionality.

## Suggestions for Copilot

- **Code Completion:** Suggest method implementations for class stubs and handling null checks for critical path sections.
- **Pattern Recognition:** Identify recurring patterns within the codebase, like utility class setups, and suggest analogous patterns in new implementations.
- **Logic Enhancement:** Assist in crafting complex logic structures such as algorithmic decision-making for random topic selection.
- **Error Checking:** Propose error-handling mechanisms within Development mode features, ensuring seamless toggling and feedback to the player.
- **Performance Considerations:** Recommend optimizations for in-game performance, especially when determining research topics or checking research prerequisites.

By leveraging these guidelines and suggestions, developers can enhance and extend the Random Research mod effectively, ensuring a stable and enjoyable player experience.
