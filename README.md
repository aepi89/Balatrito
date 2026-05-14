# BALATRITO

## Funzionalità implementate

**Carte e mazzo**
- Mazzo da 52 carte generato automaticamente da enum `Seme` × `Valore`
- Mescolata tramite algoritmo Fisher-Yates
- Riciclo automatico del mazzo quando si esaurisce

**Mano**
- Mano da 8 carte distribuite dal mazzo
- Ordinamento automatico via Bubble Sort ad ogni aggiunta
- Selezione carte con click (la carta si "alza" visivamente)

**Combinazioni rilevate**
- Scala Reale, Full House, Colore, Scala, Doppia Coppia, Tris, Coppia, Carta Alta

**Bottone Scarta**
- Scarta le carte selezionate e le sostituisce con nuove dal mazzo

**Bottone Tira**
- Gioca le carte selezionate
- Calcola la combinazione e aggiorna il punteggio cumulativo
- Controlla se il punteggio da battere è stato raggiunto

**Sistema turni**
- Tre livelli ciclici: Piccolo (100),Grande (150),Boss (200)
- Il punteggio si azzera ad ogni turno superato
- Il mazzo si rimescola ad ogni nuovo turno

---

## Come si gioca

1. All'avvio ricevi 8 carte casuali dal mazzo
2. Clicca le carte che vuoi giocare o scartare per selezionarle (si alzano)
3. Premi **Scarta** per sostituire le carte selezionate con nuove dal mazzo
4. Premi **Tira** per giocare le carte selezionate e ottenere il punteggio
5. Raggiungi il punteggio richiesto per avanzare al turno successivo

---

## Versione .NET
- **C# / .NET : 10.0**

