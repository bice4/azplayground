import React from "react";
import { createRoot } from "react-dom/client";

//theme
import "primereact/resources/themes/lara-dark-indigo/theme.css";

import MyApp from "./App";

const container = document.getElementById("root");
const root = createRoot(container);
root.render(<MyApp />);
