const express = require("express");
const fs = require("fs");
const app = express();
const PORT = 3000;
const DATA_FILE = "assets/data.json";

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// Utility function to transliterate Norwegian letters
function transliterateNorwegian(text) {
  if (typeof text !== "string") return text; // Ensure only strings are processed
  return text
    .replaceAll("æ", "ae")
    .replaceAll("Æ", "AE")
    .replaceAll("ø", "o")
    .replaceAll("Ø", "O")
    .replaceAll("å", "aa")
    .replaceAll("Å", "AA");
}

// Recursively transliterate strings within an object or array
function transliterateObject(obj) {
  if (typeof obj === "string") {
    return transliterateNorwegian(obj);
  } else if (Array.isArray(obj)) {
    return obj.map(transliterateObject);
  } else if (typeof obj === "object" && obj !== null) {
    const newObj = {};
    for (const key in obj) {
      if (obj.hasOwnProperty(key)) {
        newObj[key] = transliterateObject(obj[key]); // Apply only to values
      }
    }
    return newObj;
  }
  return obj;
}

// Read data file
const readData = () => {
  try {
    const data = fs.readFileSync(DATA_FILE, "utf8");
    return JSON.parse(data) || [];
  } catch (error) {
    console.error("Feil ved lesing av data.json:", error);
    return [];
  }
};

// Write data file
const writeData = (data) => {
  fs.writeFileSync(DATA_FILE, JSON.stringify(data, null, 2), {
    encoding: "utf8",
  });
};

// GET all people with transliteration applied
app.get("/people", (req, res) => {
  try {
    const data = readData();
    const transformedData = transliterateObject(data);
    res.setHeader("Content-Type", "application/json; charset=utf-8");
    res.json(transformedData);
  } catch (error) {
    console.error("JSON Formatting Error:", error);
    res.status(500).json({ error: "Feil ved formatering av JSON" });
  }
});

// GET a person by ID with transliteration applied
app.get("/people/:id", (req, res) => {
  const id = parseInt(req.params.id);
  const people = readData();
  const person = people.find((p) => p.id === id);
  if (person) {
    res.json(transliterateObject(person));
  } else {
    res.status(404).json({ error: "Person ikke funnet" });
  }
});

// POST - Add a new person
app.post("/people", (req, res) => {
  let people = readData();
  const newId = people.length > 0 ? people[people.length - 1].id + 1 : 1;
  const { Id, ...personData } = req.body; // Remove pre-existing 'Id' field
  const newPerson = { id: newId, ...personData };
  people.push(newPerson);
  writeData(people);
  res.status(201).json(newPerson);
});

// DELETE - Remove a person by ID
app.delete("/people/:id", (req, res) => {
  const id = parseInt(req.params.id);
  let people = readData();
  const filteredPeople = people.filter((p) => p.id !== id);
  if (people.length === filteredPeople.length) {
    return res.status(404).json({ error: "Person ikke funnet" });
  }
  writeData(filteredPeople);
  res.status(200).json({ message: `Person med ID ${id} slettet.` });
});

app.listen(PORT, () => {
  console.log(`Server kjører på http://localhost:${PORT}`);
});

process.addListener("SIGINT", async () => {
  console.log("SIGTERM signal mottatt.");
  setTimeout(() => {
    console.log("Avslutter prosessen...");
    process.exit(0);
  }, 5000);
});
