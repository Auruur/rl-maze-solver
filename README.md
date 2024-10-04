# Maze Solving with Reinforcement Learning in Unity

**Authors**: Giovanni Murgia, Pietro Cosseddu  
**Institution**: Università degli Studi di Cagliari

## Abstract

This project applies reinforcement learning principles to train an agent to solve randomly generated mazes in a 3D Unity environment using the ML-Agents library and PyTorch. The goal was to analyze the performance of the agent as maze sizes increase and evaluate the limitations of reinforcement learning in this specific context.

## Demo

[![Watch the demo](https://img.youtube.com/vi/6bJnMIckruA/maxresdefault.jpg)](https://www.youtube.com/watch?v=6bJnMIckruA)

Click the image above to watch a demo of the agent solving a randomly generated maze in Unity.

## Table of Contents

1. [Introduction](#introduction)
2. [Reinforcement Learning](#reinforcement-learning)
3. [Problem Statement](#problem-statement)
4. [Tools and Methods](#tools-and-methods)
5. [Environment Setup](#environment-setup)
6. [Agent and Training](#agent-and-training)
7. [Results](#results)
8. [Conclusions](#conclusions)
9. [How to Run](#how-to-run)

## Introduction

In this project, we explore how an agent can be trained to navigate and solve random mazes generated in Unity using reinforcement learning. We compared agent performance as the maze size increased, analyzing both the strengths and weaknesses of the reinforcement learning approach in solving increasingly complex mazes.

## Reinforcement Learning

Reinforcement learning is a machine learning paradigm in which an agent learns by interacting with its environment and receiving feedback in the form of rewards or penalties based on its actions. The agent's goal is to maximize cumulative rewards over time.

## Problem Statement

The main question we sought to answer was: _Can an agent be trained on randomly generated mazes and learn to solve new, unseen mazes efficiently?_

To address this, we generated random mazes of increasing complexity using Unity and trained an agent to navigate and find the exit. We monitored the agent's performance, especially in mazes of different sizes.

## Tools and Methods

We used several tools and libraries for this project:

- **Unity**: A 3D editor and game engine used to create the environment and visualize the agent's behavior in real-time.
- **ML-Agents**: Unity's reinforcement learning package, providing various classes and components to implement and train RL agents.
- **PyTorch**: Used to train the neural network powering the agent's decision-making process.
- **TensorBoard**: Used to monitor and visualize training metrics.

## Environment Setup

The maze environment was generated randomly using the "Hunt and Kill" algorithm. The agent, represented by a sphere, navigated through the maze, and its movement was regulated by the script `MoveAgent`. The agent was initially provided with global knowledge of the maze but later received only local observations using the Ray Perception Sensor Component in Unity.

Maze sizes ranged from **5x5** to **9x9**, and each maze was populated with obstacles, rewards, and an exit. The agent's task was to reach the exit while minimizing redundant exploration.

### Maze Generation

Mazes were generated using the **Hunt and Kill** algorithm, which starts from the top-left cell and explores neighboring unvisited cells until all cells have been visited.

- Blue cells: Unvisited
- Orange cells: Visited once
- Red cells: Visited multiple times

Additionally, small golden camels were randomly placed on some cells, serving as rewards for the agent.

## Agent and Training

The agent's movement was modeled as a continuous action space, where the agent applied force to move in different directions within the maze. The agent was trained with different observation types:

- **Global observation**: The agent had knowledge of all the walls and exit positions.
- **Local observation**: Using 8 rays to sense nearby objects and walls, the agent only had local knowledge of its surroundings.

### Rewards

- **+100** for reaching the exit.
- **+10** for collecting a bonus (golden camel).
- **+1.5** for entering a new, unvisited cell.
- **-0.5** for entering a previously visited cell.

## Results

Training was conducted across different maze sizes, with models tested and compared across 5 runs for each size. Pre-trained models from smaller mazes were fine-tuned for larger mazes, and results indicated:

- **5x5 mazes**: The agent consistently converged to a solution.
- **6x6 mazes**: Slightly lower convergence rates and more variance in results.
- **7x7 mazes**: Increased difficulty, but models pre-trained on smaller mazes performed better.
- **8x8 mazes**: High instability and increased failure to converge.
- **9x9 mazes**: Some models did not converge, but fine-tuned models performed better than training from scratch.

## Conclusions

The study shows that reinforcement learning can solve small random mazes but struggles as maze size increases. Pre-trained models improve performance when fine-tuned on larger mazes. However, reinforcement learning is not the most efficient approach for solving large mazes, where traditional pathfinding algorithms (e.g., Dijkstra) remain superior.

## How to Run

To run this project locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/username/repository-name.git
   cd repository-name
2. Install Unity and the ML-Agents package.
3. Set up the Python environment for training:
   ```bash
   pip install torch tensorboard mlagents
4. Open the Unity project and run the simulation.
5. Train the agent using ML-Agents:
   ```bash
   mlagents-learn config/trainer_config.yaml --run-id=<run-id>
6. Visualize training progress with TensorBoard:
   ```bash
   tensorboard --logdir results/

