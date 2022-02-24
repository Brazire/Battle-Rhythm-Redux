// GENERATED AUTOMATICALLY FROM 'Assets/InputControllers/Rhythm-Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @RhythmControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @RhythmControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Rhythm-Controls"",
    ""maps"": [
        {
            ""name"": ""Rhythm"",
            ""id"": ""f52bac99-f4cf-4713-98d3-5c9573ba0eab"",
            ""actions"": [
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""22a420f4-b0fd-438d-a041-23ae3c4be914"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""2d06233e-3202-4d05-84ef-c1c1d100e877"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""c772ebce-05f2-49e6-8c08-fe3a1a26c1c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""4600c703-8ea2-4e41-a4ee-8e05eb9105d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch"",
                    ""type"": ""Button"",
                    ""id"": ""c85e947d-15a2-41c9-89df-60881657b573"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fee8501a-dd13-42b5-a962-84758b9486ab"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""123e5800-bcb8-4f54-af42-e59b7ae70137"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""214adce6-58e1-4c24-8c96-299b34c0ef85"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b2b9a93-ea6b-448f-97e2-6dd773f51762"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24cc6359-1b4e-41fe-870b-2f9b13393791"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da59c643-1c45-446a-a5fb-4416a6fd3b6f"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1b80b02-d7d1-4b3f-b709-a79654268209"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2c4017e-acb9-42d9-aef1-fb1294eb4ae6"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e2922c7-8ca4-40fc-b451-23cb2c30f4d3"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5243227-d5d5-4534-b133-73c829bafbef"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""314533ad-8976-491b-9ffd-b69c824fec26"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44233a93-930e-431a-b63f-dd8acbebe415"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2dd8795-4e84-4a54-8602-8609815f7ec6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92bae31e-16e9-48b5-8e83-4aeabf27581a"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12ea80fe-d046-4ae8-b0df-a4e2539897ff"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c78474c-e996-4ed4-b967-3ff1fdb60561"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""140478c2-363c-4dbf-b37f-e7527b79a951"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b043af7e-74d5-4914-9aad-594b99b762c1"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Battle"",
            ""id"": ""87235204-710a-4359-bfe0-693c9df767ae"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""dcaf3522-e949-49d4-b1be-121912e7aa83"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""4be65086-ce37-4cdf-8aba-1e6f87a02a3b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Change"",
                    ""type"": ""Button"",
                    ""id"": ""4bc1fff1-af26-4917-8841-4ada413b2b60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skills"",
                    ""type"": ""Button"",
                    ""id"": ""e3e71fee-8cd2-4453-b8a2-3c3be7b8504a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch"",
                    ""type"": ""Button"",
                    ""id"": ""b7fd00b4-a29b-491c-b47a-897ae06d36ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""29fe5b62-f589-4a47-9ac5-d78e30586cef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""9986ea65-0341-45ac-a8ea-a909e6f269d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill3"",
                    ""type"": ""Button"",
                    ""id"": ""939c0331-7b1c-4d3d-953e-7eaafc74d087"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SkillBack"",
                    ""type"": ""Button"",
                    ""id"": ""fd63f0fc-4471-46f9-bfdb-0197d2486c42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1a8246b0-cec6-47e1-b939-1ceda173520a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c1f2009-aee5-4a3c-b5c9-ddf40c7a9ada"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e45e1bf-8107-479e-95d0-0981bbe73fcb"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24cccda4-9d04-4123-86e5-4f05bc5673fe"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2001d1f-26c7-4bab-a551-971d81e57038"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00af7630-fffb-403d-909c-331b29df5d83"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50c4d06f-a841-4efe-9e8a-5c99bdd1b8d8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4482b092-6acc-4af5-b967-7d73fd69ad2d"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47cddb15-fe7c-440a-8ae5-448acabbe216"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b09a6c62-ca22-4ca5-a385-79ffde4a74eb"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""572876f3-ea50-4f7d-ae65-3b4403f40570"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc7f373f-031a-461a-96c7-5c5caf3e6228"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90d3b0db-9543-44a4-bc66-e556a0c8c523"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skills"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e657edd-a4e6-4c0f-a1de-41b0a23dc3a4"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skills"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f78d6080-08d1-4a34-8e32-a826b2a3ddbe"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skills"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4d999d7-fdee-4db9-8db9-5834faac17ca"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skills"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14b6db29-a104-4473-9703-a7a4b4d7c84a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d223d2a8-150c-4652-bc2c-6a0a510263eb"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acdc61da-c00d-4519-a613-911c0fd1743c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c161806-df92-4223-a3f4-c9cbc9d0167b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00a2d10d-3968-4fcf-b1c8-155c7990bf1a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cdebb0e-8c58-406c-8dab-83d33635674f"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3177bed8-9b5b-42ad-b7d4-a0ab1497547d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89aa1e89-4668-4e64-85cc-cd5a67580fef"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c21a7405-8426-4937-85f4-e3c555eae823"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1264d5f4-fb76-40d4-b703-f5ebb83ab9ed"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dce38102-484c-4361-b851-cdeaefa278ea"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1a756af-4dca-4095-8f08-71e35d06495d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cf7f0dd-d531-4236-b682-0126c8eb1516"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80a87938-7414-4fc4-b858-2457f5ca66bc"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8bce7dc-bc9d-46bd-867e-8cd9f0a49f72"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0378dc8-1e60-49b3-934c-9fb211dc2fb1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05a9e3fe-0529-4b96-a3b3-67c6f256e0d4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a872f457-6202-4838-b932-599dab854920"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Rhythm
        m_Rhythm = asset.FindActionMap("Rhythm", throwIfNotFound: true);
        m_Rhythm_Left = m_Rhythm.FindAction("Left", throwIfNotFound: true);
        m_Rhythm_Down = m_Rhythm.FindAction("Down", throwIfNotFound: true);
        m_Rhythm_Up = m_Rhythm.FindAction("Up", throwIfNotFound: true);
        m_Rhythm_Right = m_Rhythm.FindAction("Right", throwIfNotFound: true);
        m_Rhythm_Switch = m_Rhythm.FindAction("Switch", throwIfNotFound: true);
        // Battle
        m_Battle = asset.FindActionMap("Battle", throwIfNotFound: true);
        m_Battle_Attack = m_Battle.FindAction("Attack", throwIfNotFound: true);
        m_Battle_Run = m_Battle.FindAction("Run", throwIfNotFound: true);
        m_Battle_Change = m_Battle.FindAction("Change", throwIfNotFound: true);
        m_Battle_Skills = m_Battle.FindAction("Skills", throwIfNotFound: true);
        m_Battle_Switch = m_Battle.FindAction("Switch", throwIfNotFound: true);
        m_Battle_Skill1 = m_Battle.FindAction("Skill1", throwIfNotFound: true);
        m_Battle_Skill2 = m_Battle.FindAction("Skill2", throwIfNotFound: true);
        m_Battle_Skill3 = m_Battle.FindAction("Skill3", throwIfNotFound: true);
        m_Battle_SkillBack = m_Battle.FindAction("SkillBack", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Rhythm
    private readonly InputActionMap m_Rhythm;
    private IRhythmActions m_RhythmActionsCallbackInterface;
    private readonly InputAction m_Rhythm_Left;
    private readonly InputAction m_Rhythm_Down;
    private readonly InputAction m_Rhythm_Up;
    private readonly InputAction m_Rhythm_Right;
    private readonly InputAction m_Rhythm_Switch;
    public struct RhythmActions
    {
        private @RhythmControls m_Wrapper;
        public RhythmActions(@RhythmControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Left => m_Wrapper.m_Rhythm_Left;
        public InputAction @Down => m_Wrapper.m_Rhythm_Down;
        public InputAction @Up => m_Wrapper.m_Rhythm_Up;
        public InputAction @Right => m_Wrapper.m_Rhythm_Right;
        public InputAction @Switch => m_Wrapper.m_Rhythm_Switch;
        public InputActionMap Get() { return m_Wrapper.m_Rhythm; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RhythmActions set) { return set.Get(); }
        public void SetCallbacks(IRhythmActions instance)
        {
            if (m_Wrapper.m_RhythmActionsCallbackInterface != null)
            {
                @Left.started -= m_Wrapper.m_RhythmActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_RhythmActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_RhythmActionsCallbackInterface.OnLeft;
                @Down.started -= m_Wrapper.m_RhythmActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_RhythmActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_RhythmActionsCallbackInterface.OnDown;
                @Up.started -= m_Wrapper.m_RhythmActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_RhythmActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_RhythmActionsCallbackInterface.OnUp;
                @Right.started -= m_Wrapper.m_RhythmActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_RhythmActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_RhythmActionsCallbackInterface.OnRight;
                @Switch.started -= m_Wrapper.m_RhythmActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_RhythmActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_RhythmActionsCallbackInterface.OnSwitch;
            }
            m_Wrapper.m_RhythmActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
            }
        }
    }
    public RhythmActions @Rhythm => new RhythmActions(this);

    // Battle
    private readonly InputActionMap m_Battle;
    private IBattleActions m_BattleActionsCallbackInterface;
    private readonly InputAction m_Battle_Attack;
    private readonly InputAction m_Battle_Run;
    private readonly InputAction m_Battle_Change;
    private readonly InputAction m_Battle_Skills;
    private readonly InputAction m_Battle_Switch;
    private readonly InputAction m_Battle_Skill1;
    private readonly InputAction m_Battle_Skill2;
    private readonly InputAction m_Battle_Skill3;
    private readonly InputAction m_Battle_SkillBack;
    public struct BattleActions
    {
        private @RhythmControls m_Wrapper;
        public BattleActions(@RhythmControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_Battle_Attack;
        public InputAction @Run => m_Wrapper.m_Battle_Run;
        public InputAction @Change => m_Wrapper.m_Battle_Change;
        public InputAction @Skills => m_Wrapper.m_Battle_Skills;
        public InputAction @Switch => m_Wrapper.m_Battle_Switch;
        public InputAction @Skill1 => m_Wrapper.m_Battle_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_Battle_Skill2;
        public InputAction @Skill3 => m_Wrapper.m_Battle_Skill3;
        public InputAction @SkillBack => m_Wrapper.m_Battle_SkillBack;
        public InputActionMap Get() { return m_Wrapper.m_Battle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleActions set) { return set.Get(); }
        public void SetCallbacks(IBattleActions instance)
        {
            if (m_Wrapper.m_BattleActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnAttack;
                @Run.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnRun;
                @Change.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnChange;
                @Change.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnChange;
                @Change.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnChange;
                @Skills.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkills;
                @Skills.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkills;
                @Skills.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkills;
                @Switch.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSwitch;
                @Skill1.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill1;
                @Skill2.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill2;
                @Skill3.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill3;
                @Skill3.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill3;
                @Skill3.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkill3;
                @SkillBack.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkillBack;
                @SkillBack.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkillBack;
                @SkillBack.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSkillBack;
            }
            m_Wrapper.m_BattleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Change.started += instance.OnChange;
                @Change.performed += instance.OnChange;
                @Change.canceled += instance.OnChange;
                @Skills.started += instance.OnSkills;
                @Skills.performed += instance.OnSkills;
                @Skills.canceled += instance.OnSkills;
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
                @Skill1.started += instance.OnSkill1;
                @Skill1.performed += instance.OnSkill1;
                @Skill1.canceled += instance.OnSkill1;
                @Skill2.started += instance.OnSkill2;
                @Skill2.performed += instance.OnSkill2;
                @Skill2.canceled += instance.OnSkill2;
                @Skill3.started += instance.OnSkill3;
                @Skill3.performed += instance.OnSkill3;
                @Skill3.canceled += instance.OnSkill3;
                @SkillBack.started += instance.OnSkillBack;
                @SkillBack.performed += instance.OnSkillBack;
                @SkillBack.canceled += instance.OnSkillBack;
            }
        }
    }
    public BattleActions @Battle => new BattleActions(this);
    public interface IRhythmActions
    {
        void OnLeft(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
    }
    public interface IBattleActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnChange(InputAction.CallbackContext context);
        void OnSkills(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnSkill3(InputAction.CallbackContext context);
        void OnSkillBack(InputAction.CallbackContext context);
    }
}
