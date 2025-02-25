const fs = require("fs");
const DATA_FILE = "assets/data.json";

// Read existing data or create an empty array if the file does not exist
if (!fs.existsSync(DATA_FILE)) {
  console.log("Oppretter data.json siden den ikke finnes...");
  fs.writeFileSync(DATA_FILE, "[]");
}

// Read and update data
fs.readFile(DATA_FILE, "utf8", (err, data) => {
  if (err) {
    console.error("Feil ved lesing av data.json:", err);
    return;
  }

  let people = [];
  if (data) {
    try {
      people = JSON.parse(data);
    } catch (parseError) {
      console.error("Feil ved parsing av JSON:", parseError);
      return;
    }
  }

  // Assign sequential numeric IDs and remove old long IDs
  people = people.map((person, index) => ({
    id: index + 1, // Start IDs from 1 and increment
    name: person.name,
    age: person.age,
    city: person.city,
    country: person.country,
  }));

  fs.writeFile(DATA_FILE, JSON.stringify(people, null, 2), (writeErr) => {
    if (writeErr) {
      console.error("Feil ved skriving til data.json:", writeErr);
    } else {
      console.log(
        "Alle personer har nå sekvensielle ID-er uten lange UUID-er."
      );
    }
  });
});
