import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import "./App.css";
import TaskList from "./TaskList/TaskList.js";
import AnimatedIcon from "./AnimatedIcon";

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<AnimatedIcon />} />
          <Route path="/tasklist" element={<TaskList listId={1} />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
