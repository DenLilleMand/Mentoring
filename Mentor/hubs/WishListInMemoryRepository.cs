using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Mentor.Models;
using Microsoft.AspNet.SignalR;
/**
 * Author: matti
 */
namespace Mentor.hubs
{
    public class WishListInMemoryRepository
    {
        private static WishListInMemoryRepository _instance = null;

        private readonly Dictionary<int, Dictionary<int, Boolean>> _wishListVoteingDictionary;
        private readonly Dictionary<int,WishListProgramJson> _wishListProgramDictionary;

        private WishListInMemoryRepository()
        {
            if (_instance != null)
            {
                throw new AccessViolationException();
            }
            _wishListVoteingDictionary = new Dictionary<int, Dictionary<int, Boolean>>();
            _wishListProgramDictionary = new Dictionary<int, WishListProgramJson>();
        }

        public static WishListInMemoryRepository GetInstance()
        {
                if (_instance == null)
                {
                    _instance = new WishListInMemoryRepository();
                }
                return _instance;
        }

        public void AddProgram(WishListProgramJson program, int userId, IHubContext context)
        {
            lock (_wishListVoteingDictionary)
            {
                Dictionary<int, Boolean> votes;
                if (!_wishListVoteingDictionary.TryGetValue(program.Id, out votes))
                {
                    _wishListVoteingDictionary.Add(program.Id, new Dictionary<int, Boolean>());
                    
                    lock (_wishListProgramDictionary)
                    {
                        _wishListProgramDictionary.Add(program.Id, program);
                        context.Clients.All.programWasAdded(program);
                    }
                }
                else
                {
                    throw new InvalidOperationException(
                        "The program has allready been created, which caused an internal mistake," +
                        "the program input was:" + program);
                }
            }
        }

        public ICollection<WishListProgramJson> GetAllWishListPrograms()
        {
            lock (_wishListProgramDictionary)
            {
                return _wishListProgramDictionary.Values;
            }
        }


        public WishListProgramJson Vote(WishListProgramJson program, int userId, Boolean userVote)
        {
            Debug.WriteLine("programId:" + program.Id);
            Debug.WriteLine("User id" + userId);
            Debug.WriteLine("Vote:" + userVote);
            lock (_wishListVoteingDictionary)
            {
                Dictionary<int, Boolean> votes;
                if (_wishListVoteingDictionary.TryGetValue(program.Id, out votes))
                {
                    
                    Boolean vote;
                    if (votes.TryGetValue(userId, out vote))
                    {
                        Debug.WriteLine("The user has voted on this program before.");
                        if (vote == userVote)
                        {
                            //do nothing
                            Debug.WriteLine("The vote we're the same as the previous.");
                        }
                        else
                        {
                            if (userVote)
                            {
                                Debug.WriteLine("The user vote was true, so the old vote mustve been false. so we count up 2 to make up for our old downvote.");
                                WishListProgramJson wishListProgramJson = _wishListProgramDictionary[program.Id];
                                if (wishListProgramJson.Votes < 99)
                                {
                                    wishListProgramJson.Votes += 2;
                                }
                                else
                                {
                                    throw new InvalidProgramException("The user tried to push the programs votes above 99, that is now allowed.");
                                }

                                return wishListProgramJson;
                            }
                            else //if uservote == false;
                            {
                                Debug.WriteLine("The user vote was false, so the old vote mustve been true. so we count down 2 to make up for our old upvote.");
                                WishListProgramJson wishListProgramJson = _wishListProgramDictionary[program.Id];
                                if (wishListProgramJson.Votes > -9)
                                {
                                    _wishListProgramDictionary[program.Id].Votes -= 2;
                                }
                                else
                                {
                                    throw new InvalidProgramException("The user tried to push the programs votes below -9, that is now allowed.");
                                }
                                return wishListProgramJson;
                            }
                            
                        }
                        
                    }
                    else
                    {
                        Debug.WriteLine("the user hadnt voted on this program before. so we figure out if the vote is true or false," +
                                        "add the userid and user vote to the hashmap. and return the new program.");
                        votes.Add(userId, userVote);
                        if (userVote)
                        {
                            WishListProgramJson wishListProgramJson = _wishListProgramDictionary[program.Id];
                            if (wishListProgramJson.Votes < 99)
                            {
                                wishListProgramJson.Votes += 1;
                            }
                            else
                            {
                                throw new InvalidProgramException("The user tried to push the programs votes above 99, that is now allowed.");
                            }
                            return wishListProgramJson;
                        }
                        else //if vote == false;
                        {
                            WishListProgramJson wishListProgramJson = _wishListProgramDictionary[program.Id];
                            if (wishListProgramJson.Votes > -9)
                            {
                                _wishListProgramDictionary[program.Id].Votes -= 1;
                            }
                            else
                            {
                                throw new InvalidProgramException("The user tried to push the programs votes below -9, that is now allowed.");
                            }
                            return wishListProgramJson;
                        }
                        
                    }
                }
                Debug.WriteLine("Program didn't exist?");
            }
            return null;
        }


        public WishListProgramJson OldVote(WishListProgramJson program, int userId,Boolean userVote)
        {
            lock (_wishListVoteingDictionary)
            {
                Dictionary<int, Boolean> votes;
                if (_wishListVoteingDictionary.TryGetValue(program.Id, out votes))
                {
                    Boolean vote;
                    if (votes.TryGetValue(userId, out vote))
                    {
                        lock (_wishListProgramDictionary)//are we allowed to lock two resources? doesnt that give a deadlock, unless we have release?
                        {
                            if ((vote && userVote) || (!vote && !userVote))
                            {
                                Debug.WriteLine("the vote we received was the same as the previous vote, so we're ignoreing the vote.");
                                //do nothing
                                return null;
                            }
                            if (userVote)
                            {
                                WishListProgramJson wishListProgramJson = _wishListProgramDictionary[program.Id];
                                if (wishListProgramJson.Votes < 99)
                                {
                                    wishListProgramJson.Votes += 2;
                                }
                                else
                                {
                                    throw new InvalidProgramException("The user tried to push the programs votes above 99, that is now allowed.");
                                }
                                return wishListProgramJson;
                            }
                            else //user vote == false
                            {
                                 WishListProgramJson wishListProgramJson = _wishListProgramDictionary[program.Id];
                                if (wishListProgramJson.Votes > -9)
                                {
                                    _wishListProgramDictionary[program.Id].Votes -= 2;    
                                }
                                else
                                {
                                    throw new InvalidProgramException("The user tried to push the programs votes below -9, that is now allowed.");
                                }
                                return wishListProgramJson;
                            }
                        }
                    }
                    else
                    {
                        if (userVote)
                        {
                            votes.Add(userId, true);
                             WishListProgramJson wishListProgramJson = _wishListProgramDictionary[program.Id];
                                if (wishListProgramJson.Votes < 99)
                                {
                                    wishListProgramJson.Votes += 1;
                                }
                                else
                                {
                                    throw new InvalidProgramException("The user tried to push the programs votes above 99, that is now allowed.");
                                }
                            return wishListProgramJson;
                        }
                        else
                        {
                            votes.Add(userId, false);
                             WishListProgramJson wishListProgramJson = _wishListProgramDictionary[program.Id];
                                if (wishListProgramJson.Votes > -9)
                                {
                                    _wishListProgramDictionary[program.Id].Votes -= 2;    
                                }
                                else
                                {
                                    throw new InvalidProgramException("The user tried to push the programs votes below -9, that is now allowed.");
                                }
                                return wishListProgramJson;
                        }
                        
                    }
                }
                else
                {
                    Debug.WriteLine("The program wasnt found in the dictionary, which caused an internal mistake," +
                        "the program input was:" + program);
                    return null;
                }
            }
            return null;
        }
}
}