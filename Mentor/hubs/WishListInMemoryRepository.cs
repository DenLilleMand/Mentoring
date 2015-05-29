using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;
using Microsoft.AspNet.SignalR;

namespace Mentor.hubs
{
    public class WishListInMemoryRepository
    {
        private static WishListInMemoryRepository _instance = null;

        private readonly Dictionary<int, Dictionary<int, bool>> _wishListVoteingDictionary;
        private readonly Dictionary<int,WishListProgramJson> _wishListProgramDictionary;

        private WishListInMemoryRepository()
        {
            if (_instance != null)
            {
                throw new AccessViolationException();
            }
            _wishListVoteingDictionary = new Dictionary<int, Dictionary<int, bool>>();
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
                Dictionary<int, bool> votes;
                if (!_wishListVoteingDictionary.TryGetValue(program.Id, out votes))
                {
                    _wishListVoteingDictionary.Add(program.Id, new Dictionary<int, bool>());
                    
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
            //Vote(program, userId);
        }

        public ICollection<WishListProgramJson> GetAllWishListPrograms()
        {
            lock (_wishListProgramDictionary)
            {
                return _wishListProgramDictionary.Values;
            }
        } 

        public void Vote(WishListProgramJson program, int userId)
        {
            lock (_wishListVoteingDictionary)
            {
                Dictionary<int, bool> votes;
                if (_wishListVoteingDictionary.TryGetValue(program.Id, out votes))
                {
                    bool vote;
                    if (votes.TryGetValue(userId, out vote))
                    {
                        vote = program.Voted;
                    }
                    else
                    {
                        votes.Add(userId, program.Voted);
                    }
                    lock (_wishListProgramDictionary)
                    {
                        if (program.Voted)
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
                        }
                        else //if program.Void == false
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
                            
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException(
                        "The program wasnt found in the dictionary, which caused an internal mistake," +
                        "the program input was:" + program);
                }
            }
        }

        //program was accepted. 



}
}