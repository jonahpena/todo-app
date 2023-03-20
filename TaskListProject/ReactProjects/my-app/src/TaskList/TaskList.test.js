import { render, screen, fireEvent } from "@testing-library/react";
import TaskList from "./TaskList";

// Mock fetch API to avoid making real API calls
global.fetch = jest.fn(() =>
  Promise.resolve({
    json: () => Promise.resolve([]),
  })
);

describe("TaskList", () => {
  beforeEach(() => {
    fetch.mockClear();
  });

  test("renders TaskList component", () => {
    render(<TaskList />);
    expect(screen.getByText(/Enter your task/i)).toBeInTheDocument();
  });

  // Add more tests to cover the component's functionality
});
